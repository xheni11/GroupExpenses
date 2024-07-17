using GroupExpenses.BLL.ViewModels.User;

namespace GroupExpenses.BLL.IServices
{
    public interface IUserService
   {
      Task<IEnumerable<GetUserViewModel>> GetAll();
      Task<GetUserViewModel> GetById(int id);
      Task<GetUserViewModel> Add(AddUserViewModel userViewModel);
      Task<GetUserViewModel> Update(UpdateUserViewModel userViewModel);
      Task Delete(int id);
   }
}
