using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace GroupExpenses.Controllers
{
    [ApiController]
   [Route("[controller]")]
   public class EventController: ControllerBase
   {

      private readonly ILogger<EventController> _logger;
      private readonly IEventService _eventService;
      public EventController(ILogger<EventController> logger,IEventService eventService)
      {
         _logger = logger;
         _eventService = eventService;
      }

      [HttpGet]
      public async Task<IActionResult> GetEventsByUser()
      {
         // TODO get loggedIn userId from app context after implementation of auth
         return Ok(await _eventService.GetEventsByUser(3));
      }

      [HttpGet("{eventId}")]
      public async Task<IActionResult> GetEvent([FromRoute] int eventId)
      {
         return Ok(await _eventService.GetEvent(eventId));
      }

      [HttpPost]
      public async Task<IActionResult> Add([FromBody] AddEventViewModel eventToAdd)
      {
         return Ok(await _eventService.Add(eventToAdd));
      }

      [HttpPut]
      public async Task<IActionResult> Update([FromBody] UpdateEventViewModel eventToUpdate)
      {
         return Ok(await _eventService.Update(eventToUpdate));
      }

      [HttpDelete("{eventId}")]
      public async Task<IActionResult> Delete([FromRoute] int eventId)
      {
         await _eventService.Delete(eventId);
         return Ok() ;
      }
   }
}
