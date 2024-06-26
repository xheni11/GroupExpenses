using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.ViewModels;
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
      public async Task<IEnumerable<EventViewModel>> GetEventsByUser()
      {
         // TODO get loggedIn userId from app context after implementation of auth
         return await _eventService.GetEventsByUser(3);
      }

      [HttpGet("{eventId}")]
      public async Task<EventViewModel> GetEvent([FromRoute] int eventId)
      {
         return await _eventService.GetEvent(eventId);
      }

      [HttpPost]
      public async Task<EventViewModel> Add([FromBody] EventViewModel eventToAdd)
      {
         return await _eventService.Add(eventToAdd);
      }

      [HttpPut]
      public async Task<EventViewModel> Update([FromBody] EventViewModel eventToUpdate)
      {
         return await _eventService.Update(eventToUpdate);
      }

      [HttpDelete("{eventId}")]
      public async Task<bool> Delete([FromRoute] int eventId)
      {
         await _eventService.Delete(eventId);
         // TODO add a response view model 
         return true ;
      }
   }
}
