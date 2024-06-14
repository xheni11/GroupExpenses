using GroupExpenses.DTOs;

namespace GroupExpenses.BLL.DTOs
{
   public class EventViewModel
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Details { get; set; }
      public string? Location { get; set; }
      public IEnumerable<UserViewModel> Participants { get; set; }
   }
}
