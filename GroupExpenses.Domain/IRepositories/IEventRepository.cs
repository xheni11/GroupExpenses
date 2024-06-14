using GroupExpenses.Domain.Entities;

namespace GroupExpenses.Domain.IRepositories
{
   public interface IEventRepository
   {
      Task<Event> GetById(int id);
      Task<IEnumerable<UserEvent>> GetEventsByUser(int userId);
      Task<Event>Add(Event eventToAdd);
      Task Delete(int id);
      Task Update(Event eventToUpdate);
   }
}
