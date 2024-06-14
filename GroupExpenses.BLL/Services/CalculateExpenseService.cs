using GroupExpenses.APIGatway;
using GroupExpenses.DTOs;
using GroupExpenses.Enums;

namespace GroupExpenses.Services
{
   public class CalculateExpenseService
   {
      private readonly ExchangeRateAPIService _exchangeRateAPIService;
        public CalculateExpenseService(ExchangeRateAPIService exchangeRateAPIService)
        {
               _exchangeRateAPIService = exchangeRateAPIService;
        }
        private List<ReceiptViewModel> ReceiptList {get; set;}
      public async Task AddReceipt(ReceiptViewModel receipt)
      {
         //receipt.PriceInEur = await GetPriceInEur(receipt.Price,receipt.Currency);
         //receipt.PaidBy.TotalDept -= receipt.PriceInEur;
         //var paidForCount = receipt.PaidFor.Count();
         //foreach (var user in receipt.PaidFor)
         //{
         //   user.TotalDept = +receipt.PriceInEur/paidForCount;
         //}
         //ReceiptList.Add(receipt);
      }

      public double GetTotalExpenses()
      {
         return ReceiptList.Select(x => x.PriceInEur).Sum();
      }

      public double GetTotalExpensesPaidBy(UserViewModel user)
      {
         return ReceiptList
            .Where(x => x.PaidBy == user)
            .Select(x => x.PriceInEur)
            .Sum();
      }
      public double GetTotalExpensesPaidFor(UserViewModel user)
      {
         return ReceiptList
            .Where(x => x.PaidFor == user)
            .Select(x => x.PriceInEur)
            .Sum();
      }

      private async Task<double> GetPriceInEur(double price, Currency currency)
      {
         var currencyRate = await _exchangeRateAPIService.GetExchangeRate(currency);
         return price * currencyRate.rates.EUR;
      }



   }
}
