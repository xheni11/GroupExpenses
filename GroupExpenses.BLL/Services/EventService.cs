using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Mappers;
using GroupExpenses.BLL.ViewModels;
using GroupExpenses.Domain.IRepositories;


namespace GroupExpenses.BLL.Services
{
   public class EventService: IEventService 
   {
      private readonly IEventRepository _eventRepository;

      public EventService(IEventRepository eventRepository)
      {
         _eventRepository = eventRepository;
      }
      public async Task<IEnumerable<EventViewModel>> GetEventsByUser(int userId)
      {
         var userEvents = await _eventRepository.GetEventsByUser(userId);
         return userEvents.Select(e => Mapper.ToEventViewModel(e.Event));
      }

      public async Task<EventViewModel> Add(EventViewModel eventViewModel)
      {
         var addedEvent = await _eventRepository.Add(Mapper.ToEventEntity(eventViewModel));
         return Mapper.ToEventViewModel(addedEvent);
      }
      public async Task<EventViewModel> Update(EventViewModel eventViewModel)
      {
         await _eventRepository.Update(Mapper.ToEventEntity(eventViewModel));
         return await GetEvent(eventViewModel.Id);
      }
      public async Task Delete(int eventId)
      {
         await _eventRepository.Delete(eventId);
      }

      public async Task<EventViewModel> GetEvent(int eventId)
      {
         var eventEntity = await _eventRepository.GetById(eventId);
         return Mapper.ToEventViewModel(eventEntity);
      }
   }
}
