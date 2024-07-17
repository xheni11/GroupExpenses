using GroupExpenses.BLL.ViewModels.Event;
using GroupExpenses.BLL.ViewModels.Receipt;
using GroupExpenses.Domain.Entities;
using GroupExpenses.Enums;


namespace GroupExpenses.BLL.Mappers
{
    public static class ReceiptMapper
    {

      public static Receipt ToEntity(AddReceiptViewModel receipt, decimal priceInEur)
      {
         return new Receipt
         {
            Currency = (int)receipt.Currency,
            Details = receipt.Description,
            EventId = receipt.EventId,
            Name = receipt.Name,
            Price = receipt.Price,
            PriceInEur = priceInEur
         };
      }

      public static Receipt ToEntity(UpdateReceiptViewModel receipt, decimal priceInEur)
      {
         return new Receipt
         {
            Id = receipt.Id,
            Currency = (int)receipt.Currency,
            Details = receipt.Description,
            EventId = receipt.EventId,
            Name = receipt.Name,
            Price = receipt.Price,
            PriceInEur = priceInEur
         };
      }

      public static GetReceiptViewModel ToViewModel(Receipt receipt)
      {
         return new GetReceiptViewModel
         {
            Id = receipt.Id,
            Currency = (Currency)receipt.Currency,
            Description = receipt.Details,
            EventId = receipt.EventId,
            Name = receipt.Name,
            PaidBy = UserMapper.ToViewModel(receipt.PaidBy),
            Price = receipt.Price,
            PriceInEur = receipt.PriceInEur
         };
      }

      public static IEnumerable<GetReceiptViewModel> ToViewModel(IEnumerable<Receipt> receipts)
      {
         return receipts.Select(r => ToViewModel(r));
      }
   }
}
