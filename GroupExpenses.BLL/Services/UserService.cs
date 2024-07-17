using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Mappers;
using GroupExpenses.BLL.ViewModels.User;
using GroupExpenses.Domain.IRepositories;


namespace GroupExpenses.BLL.Services
{
    public class UserService: IUserService 
   {
      private readonly IUserRepository _userRepository;

      public UserService(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }
      public async Task<IEnumerable<GetUserViewModel>> GetAll()
      {
         var users = await _userRepository.GetUsers();
         return users.Select(u => UserMapper.ToViewModel(u));
      }

      public async Task<GetUserViewModel> Add(AddUserViewModel userViewModel)
      {
         var addedUser = await _userRepository.Add(UserMapper.ToEntity(userViewModel));
         return UserMapper.ToViewModel(addedUser);
      }
      public async Task<GetUserViewModel> Update(UpdateUserViewModel userViewModel)
      {
         await _userRepository.Update(UserMapper.ToEntity(userViewModel));
         return await GetById(userViewModel.Id);
      }
      public async Task Delete(int userId)
      {
         await _userRepository.Delete(userId);
      }

      public async Task<GetUserViewModel> GetById(int userId)
      {
         var user = await _userRepository.GetById(userId);
         return UserMapper.ToViewModel(user);
      }
   }
}
