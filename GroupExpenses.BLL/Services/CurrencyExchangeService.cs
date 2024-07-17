using GroupExpenses.APIGatway;
using GroupExpenses.BLL.IServices;
using GroupExpenses.Enums;

namespace GroupExpenses.BLL.Services
{
   public class CurrencyExchangeService : ICurrencyExchangeService
   {
      private readonly ExchangeRateAPIService _exchangeRateAPIService;
      public CurrencyExchangeService(ExchangeRateAPIService exchangeRateAPIService)
      {
         _exchangeRateAPIService = exchangeRateAPIService;
      }

      public async Task<decimal> ConvertPriceInEur(decimal price, Currency currency)
      {
         var exchangeRates = await _exchangeRateAPIService.GetExchangeRate(currency);
         return price * (decimal)exchangeRates.rates.EUR;
      }
   }
}
