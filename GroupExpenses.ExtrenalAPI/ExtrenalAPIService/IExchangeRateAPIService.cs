using GroupExpenses.Enums;

namespace GroupExpenses.ExtrenalAPI.ExtrenalAPIService
{
   public interface IExchangeRateAPIService
   {
      Task<ExchangeRatesResponse> GetExchangeRate(Currency currency);
   }
}
