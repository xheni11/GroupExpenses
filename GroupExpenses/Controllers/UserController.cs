using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.ViewModels;
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
      public async Task<IEnumerable<UserViewModel>> GetAll()
      {
         return await _userService.GetAll();
      }

      [HttpGet("{userId}")]
      public async Task<UserViewModel> GetUserById([FromRoute] int userId)
      {
         return await _userService.GetById(userId);
      }

      [HttpPost]
      public async Task<UserViewModel> Add([FromBody] UserViewModel user)
      {
         return await _userService.Add(user);
      }

      [HttpPut]
      public async Task<UserViewModel> Update([FromBody] UserViewModel user)
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
