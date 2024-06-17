using GroupExpenses.BLL.ViewModels;

namespace GroupExpenses.BLL.IServices
{
   public interface IEventService
   {
      Task<IEnumerable<EventViewModel>> GetEventsByUser(int userId);
      Task<EventViewModel> GetEvent(int eventId);
      Task<EventViewModel> Add(EventViewModel eventViewModel);
      Task<EventViewModel> Update(EventViewModel eventViewModel);
      Task Delete(int eventId);
   }
}
