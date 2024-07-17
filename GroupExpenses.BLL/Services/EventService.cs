using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Mappers;
using GroupExpenses.BLL.ViewModels;
using GroupExpenses.BLL.ViewModels.Event;
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
      public async Task<IEnumerable<GetEventViewModel>> GetEventsByUser(int userId)
      {
         var userEvents = await _eventRepository.GetEventsByUser(userId);
         return userEvents.Select(e => EventMapper.ToViewModel(e.Event));
      }

      public async Task<GetEventViewModel> Add(AddEventViewModel eventViewModel)
      {
         var addedEvent = await _eventRepository.Add(EventMapper.ToEntity(eventViewModel));
         return EventMapper.ToViewModel(addedEvent);
      }
      public async Task<GetEventViewModel> Update(UpdateEventViewModel eventViewModel)
      {
         await _eventRepository.Update(EventMapper.ToEntity(eventViewModel));
         return await GetEvent(eventViewModel.Id);
      }
      public async Task Delete(int eventId)
      {
         await _eventRepository.Delete(eventId);
      }

      public async Task<GetEventViewModel> GetEvent(int eventId)
      {
         var eventEntity = await _eventRepository.GetById(eventId);
         return EventMapper.ToViewModel(eventEntity);
      }
   }
}
