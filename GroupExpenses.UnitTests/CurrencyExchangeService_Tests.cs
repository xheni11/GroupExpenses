using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Services;
using GroupExpenses.Enums;
using GroupExpenses.ExtrenalAPI.ExtrenalAPIService;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;

namespace GroupExpenses.UnitTests
{
   public class CurrencyExchangeService_Tests
   {
      private readonly Mock<IExchangeRateAPIService> _exchangeRateAPIServiceMock;
      private readonly IMemoryCache _memoryCache;
      private readonly Mock<IConfiguration> _configMock;
      private readonly ICurrencyExchangeService _currencyExchangeService;

      public CurrencyExchangeService_Tests()
      {
         _exchangeRateAPIServiceMock = new Mock<IExchangeRateAPIService>();
         _memoryCache = new MemoryCache(new MemoryCacheOptions());
         _configMock = new Mock<IConfiguration>();
         _configMock.SetupAllProperties();

         _configMock.Object["CacheOptions:AbsoluteExpirationRelativeToNow"]= "1220";

         _currencyExchangeService = new CurrencyExchangeService(
             _exchangeRateAPIServiceMock.Object,
             _memoryCache,
             _configMock.Object);
      }

      [Fact]
      public async Task ConvertPriceInEur_CurrencyRateCached_ReturnsCachedRate()
      {
         // Arrange
         decimal price = 100m;
         Currency currency = Currency.USD;
         string currencyName = Enum.GetName(typeof(Currency),currency);
         decimal cachedRate = 1.2m;

         _memoryCache.Set(currencyName,cachedRate);

         // Act
         var result = await _currencyExchangeService.ConvertPriceInEur(price,currency);

         // Assert
         Assert.Equal(price * cachedRate,result);
      }

      [Fact]
      public async Task ConvertPriceInEur_CurrencyRateNotCached_FetchesFromAPIAndCachesIt()
      {
         // Arrange
         decimal price = 100m;
         Currency currency = Currency.USD;
         string currencyName = Enum.GetName(typeof(Currency),currency);
         decimal fetchedRate = 1.2m;

         var exchangeRateResponse = new ExchangeRatesResponse
         {
            rates = new Rates { EUR = (double)fetchedRate }
         };

         _exchangeRateAPIServiceMock
             .Setup(service => service.GetExchangeRate(currency))
             .ReturnsAsync(exchangeRateResponse);

         // Act
         var result = await _currencyExchangeService.ConvertPriceInEur(price,currency);

         // Assert
         Assert.Equal(price * fetchedRate,result);
         Assert.True(_memoryCache.TryGetValue(currencyName,out decimal cachedRate));
         Assert.Equal(fetchedRate,cachedRate);
      }

      [Fact]
      public async Task ConvertPriceInEur_InvalidCurrency_ThrowsArgumentException()
      {
         // Arrange
         decimal price = 100m;
         Currency invalidCurrency = (Currency)(-1);

         // Act & Assert
         var exception = await Assert.ThrowsAsync<ArgumentException>(() => _currencyExchangeService.ConvertPriceInEur(price,invalidCurrency));
         Assert.Contains("Invalid currency",exception.Message);
      }
   
}
}
