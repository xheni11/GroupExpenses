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
      public async Task<IActionResult> GetAll()
      {
         return Ok(await _userService.GetAll());
      }

      [HttpGet("{userId}")]
      public async Task<IActionResult> GetUserById([FromRoute] int userId)
      {
         return Ok(await _userService.GetById(userId));
      }

      [HttpPost]
      public async Task<IActionResult> Add([FromBody] AddUserViewModel user)
      {
         return Ok(await _userService.Add(user));
      }

      [HttpPut]
      public async Task<IActionResult> Update([FromBody] UpdateUserViewModel user)
      {
         return Ok(await _userService.Update(user));
      }

      [HttpDelete("{userId}")]
      public async Task<IActionResult> Delete([FromRoute] int userId)
      {
         await _userService.Delete(userId);
         return Ok() ;
      }
   }
}
