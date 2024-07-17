
using GroupExpenses.BLL.ViewModels.User;

namespace GroupExpenses.BLL.ViewModels.Event
{
    public record GetEventViewModel
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Details { get; set; }
      public string? Location { get; set; }
      public IEnumerable<GetUserViewModel> Participants { get; set; }
   }
}
