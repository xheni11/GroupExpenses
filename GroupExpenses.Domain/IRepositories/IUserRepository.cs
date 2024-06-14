using GroupExpenses.Domain.Entities;

namespace GroupExpenses.Domain.IRepositories
{
   public interface IUserRepository
   {
      Task<User> GetById(int id);
      Task<IEnumerable<User>> GetUsers();
      Task<User>Add(User user);
      Task Delete(int id);
      Task Update(User user);
   }
}
