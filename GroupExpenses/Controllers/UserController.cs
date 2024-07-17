using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace GroupExpenses.Controllers
{
    [ApiController]
   [Route("[controller]")]
   public class UserController: ControllerBase
   {

      private readonly ILogger<EventController> _logger;
      private readonly IUserService _userService;
      public UserController(ILogger<EventController> logger,IUserService userService)
      {
         _logger = logger;
         _userService = userService;
      }

      [HttpGet]
      public async Task<IEnumerable<GetUserViewModel>> GetAll()
      {
         return await _userService.GetAll();
      }

      [HttpGet("{userId}")]
      public async Task<GetUserViewModel> GetUserById([FromRoute] int userId)
      {
         return await _userService.GetById(userId);
      }

      [HttpPost]
      public async Task<GetUserViewModel> Add([FromBody] AddUserViewModel user)
      {
         return await _userService.Add(user);
      }

      [HttpPut]
      public async Task<GetUserViewModel> Update([FromBody] UpdateUserViewModel user)
      {
         return await _userService.Update(user);
      }

      [HttpDelete("{userId}")]
      public async Task<bool> Delete([FromRoute] int userId)
      {
         await _userService.Delete(userId);
         return true ;
      }
   }
}
