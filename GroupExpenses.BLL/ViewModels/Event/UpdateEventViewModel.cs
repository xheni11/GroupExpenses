
namespace GroupExpenses.BLL.ViewModels.Event
{
   public record UpdateEventViewModel
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Details { get; set; }
      public string? Location { get; set; }
   }
}
