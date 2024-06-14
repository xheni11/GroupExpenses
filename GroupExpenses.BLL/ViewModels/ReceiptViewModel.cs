using GroupExpenses.Enums;

namespace GroupExpenses.BLL.ViewModels
{
   public class ReceiptViewModel
   {
      public int Id { get; set; }
      public int EventId { get; set; }
      public string Name { get; set; }
      public double Price { get; set; }
      public double PriceInEur { get; set; }
      public string Description { get; set; }
      public Currency Currency { get; set; }
      public UserViewModel PaidBy { get; set; }
      public IEnumerable<UserViewModel> PaidFor { get; set; }

   }
}
