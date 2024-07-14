using GroupExpenses.Enums;

namespace GroupExpenses.BLL.ViewModels
{
   public record ReceiptViewModel
   {
      public int Id { get; set; }
      public int EventId { get; set; }
      public string Name { get; set; }
      public decimal Price { get; set; }
      public decimal PriceInEur { get; set; }
      public string Description { get; set; }
      public Currency Currency { get; set; }
      public UserViewModel PaidBy { get; set; }
      public IEnumerable<UserViewModel> PaidFor { get; set; }

   }
}
