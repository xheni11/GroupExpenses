
using GroupExpenses.BLL.ViewModels;

namespace GroupExpenses.BLL.IServices
{
   public interface IUserService
   {
      Task<IEnumerable<UserViewModel>> GetAll();
      Task<UserViewModel> GetById(int id);
      Task<UserViewModel> Add(UserViewModel userViewModel);
      Task<UserViewModel> Update(UserViewModel userViewModel);
      Task Delete(int id);
   }
}
