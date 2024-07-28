using GroupExpenses.BLL.IServices;
using GroupExpenses.Enums;
using GroupExpenses.ExtrenalAPI.ExtrenalAPIService;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace GroupExpenses.BLL.Services
{
   public class CurrencyExchangeService : ICurrencyExchangeService
   {
      private readonly IExchangeRateAPIService _exchangeRateAPIService;
      private readonly IMemoryCache _memoryCache;
      private readonly IConfiguration _config;
      private readonly MemoryCacheEntryOptions _options;
      private const int MINUTES_PER_HOUR = 60;
      public CurrencyExchangeService(IExchangeRateAPIService exchangeRateAPIService,IMemoryCache memoryCache,IConfiguration config)
      {
         _exchangeRateAPIService = exchangeRateAPIService;
         _memoryCache = memoryCache;
         _config = config;
         _options = new MemoryCacheEntryOptions
         {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(
               Double.TryParse(_config["CacheOptions:AbsoluteExpirationRelativeToNow"],out var absoluteExpiration)
                   ? absoluteExpiration
                   : MINUTES_PER_HOUR)
         };
      }

      public async Task<decimal> ConvertPriceInEur(decimal price,Currency currency)
      {
         var currencyName = Enum.GetName(typeof(Currency),currency) ??
            throw new ArgumentException($"Invalid currency: {currency}",nameof(currency));

         if (!_memoryCache.TryGetValue(currencyName,out decimal eurRate))
         {
            var exchangeRates = await _exchangeRateAPIService.GetExchangeRate(currency);
            eurRate = (decimal)exchangeRates.rates.EUR;
            _memoryCache.Set(currencyName,eurRate,_options);
         }

         return price * eurRate;
      }
   }
}
