using GroupExpenses.Domain.Entities;
using GroupExpenses.Domain.IRepositories;
using GroupExpenses.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GroupExpenses.Domain.Repositories
{
   public class EventRepository:IEventRepository
   {
        private readonly DbSet<Event> _event;
        private readonly DbSet<UserEvent> _userEvent;
        private readonly GroupExpensesContext _context;
        public EventRepository(GroupExpensesContext context)
        {
            _context = context;
            _event = context.Set<Event>();
            _userEvent = context.Set<UserEvent>();
        }
        public async Task<Event> GetById(int id)
        {   
            return await _event.FindAsync(id);
        }

      public async Task<IEnumerable<UserEvent>> GetEventsByUser(int userId)
      {
         return await _userEvent.Where(x=> x.ParticipantId == userId).ToListAsync();
      }

      public async Task<Event> Add(Event eventToAdd)
      {
         var addedEvent = await _event.AddAsync(eventToAdd);
         await  _context.SaveChangesAsync();
         return addedEvent.Entity;
      }
     
      public async Task Delete(int id)
      {
         var eventToBeDeleted = await _event.FindAsync(id);
         if (eventToBeDeleted != null)
         {
            _event.Remove(eventToBeDeleted);
         }
         await _context.SaveChangesAsync();
      }

      public async Task Update(Event eventToUpdate)
      {
         _event.Attach(eventToUpdate);
         _context.Entry(eventToUpdate).State = EntityState.Modified;
         await _context.SaveChangesAsync();
      }
   }
}
