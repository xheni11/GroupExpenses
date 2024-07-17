using GroupExpenses.BLL.ViewModels;
using GroupExpenses.BLL.ViewModels.Event;
using GroupExpenses.Domain.Entities;

namespace GroupExpenses.BLL.Mappers
{
   public static class EventMapper
   {
      public static GetEventViewModel ToViewModel(Event entityEvent)
      {
         return new GetEventViewModel
         {
            Details = entityEvent.Details,
            Id = entityEvent.Id,
            Location = entityEvent.Location,
            Name = entityEvent.Name,
            Participants = entityEvent.Participants.Select(u => UserMapper.ToViewModel(u))
         };
      }

      public static Event ToEntity(UpdateEventViewModel entityEvent)
      {
         return new Event
         {
            Id = entityEvent.Id,
            Details = entityEvent.Details,
            Location = entityEvent.Location,
            Name = entityEvent.Name
         };
      }

      public static Event ToEntity(AddEventViewModel eventViewModel)
      {
         return new Event
         {
            Details = eventViewModel.Details,
            Location = eventViewModel.Location,
            Name = eventViewModel.Name
         };
      }
      public static IEnumerable<GetEventViewModel> ToViewModel(IEnumerable<Event> events)
      {
         return events.Select(e => ToViewModel(e));
      }
   }
}
