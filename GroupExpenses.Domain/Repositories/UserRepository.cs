using GroupExpenses.Domain.Entities;
using GroupExpenses.Domain.IRepositories;
using GroupExpenses.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GroupExpenses.Domain.Repositories
{
   public class UserRepository:IUserRepository
   {
        private readonly DbSet<User> _user;
        private readonly GroupExpensesContext _context;
        public UserRepository(GroupExpensesContext context)
        {
            _context = context;
            _user = context.Set<User>();
        }
        public async Task<User> GetById(int id)
        {   
            return await _user.FindAsync(id);
        }
      public async Task<IEnumerable<User>> GetUsers()
      {
         return await _user.ToListAsync();
      }
      public async Task<User> Add(User user)
      {
         var addedUser = await _user.AddAsync(user);
         await  _context.SaveChangesAsync();
         return addedUser.Entity;
      }
     
      public async Task Delete(int id)
      {
         var userToDelete = await _user.FindAsync(id);
         if (userToDelete != null)
         {
            _user.Remove(userToDelete);
         }
         await _context.SaveChangesAsync();
      }

      public async Task Update(User user)
      {
         _user.Attach(user);
         _context.Entry(user).State = EntityState.Modified;
         await _context.SaveChangesAsync();
      }
   }
}
