using GroupExpenses.BLL.ViewModels;
using GroupExpenses.BLL.ViewModels.Event;

namespace GroupExpenses.BLL.IServices
{
    public interface IEventService
   {
      Task<IEnumerable<GetEventViewModel>> GetEventsByUser(int userId);
      Task<GetEventViewModel> GetEvent(int eventId);
      Task<GetEventViewModel> Add(AddEventViewModel eventViewModel);
      Task<GetEventViewModel> Update(UpdateEventViewModel eventViewModel);
      Task Delete(int eventId);
   }
}
