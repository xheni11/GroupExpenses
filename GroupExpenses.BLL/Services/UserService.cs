using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Mappers;
using GroupExpenses.BLL.ViewModels;
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
      public async Task<IEnumerable<UserViewModel>> GetAll()
      {
         var users = await _userRepository.GetUsers();
         return users.Select(u => Mapper.ToUserViewModel(u));
      }

      public async Task<UserViewModel> Add(UserViewModel userViewModel)
      {
         var addedUser = await _userRepository.Add(Mapper.ToUserEntity(userViewModel));
         return Mapper.ToUserViewModel(addedUser);
      }
      public async Task<UserViewModel> Update(UserViewModel userViewModel)
      {
         await _userRepository.Update(Mapper.ToUserEntity(userViewModel));
         return await GetById(userViewModel.Id);
      }
      public async Task Delete(int userId)
      {
         await _userRepository.Delete(userId);
      }

      public async Task<UserViewModel> GetById(int userId)
      {
         var user = await _userRepository.GetById(userId);
         return Mapper.ToUserViewModel(user);
      }
   }
}
