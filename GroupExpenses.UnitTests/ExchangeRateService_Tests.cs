using GroupExpenses.APIGatway;
using GroupExpenses.Config;
using GroupExpenses.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace GroupExpenses.UnitTests
{
   public class ExchangeRateService_Tests
   {
      private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
      private readonly Mock<HttpClient> _httpClientMock;
      private readonly Mock<ExternalApiSettings> _apiSettingsMock;
      private readonly Mock<IOptions<ExternalApiSettings>> _optionsMock;
      private readonly ExchangeRateAPIService _exchangeRateService;

      public ExchangeRateService_Tests()
      {
         _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
         _httpClientMock = new Mock<HttpClient>(_httpMessageHandlerMock.Object);
         _apiSettingsMock = new Mock<ExternalApiSettings>();
         _apiSettingsMock.SetupAllProperties();

         _apiSettingsMock.Object.ExchangeRateUrl = "https://open.er-api.com/v6/latest/";
         _optionsMock = new Mock<IOptions<ExternalApiSettings>>();
         _optionsMock.Setup(x => x.Value).Returns(_apiSettingsMock.Object);

         _exchangeRateService = new ExchangeRateAPIService(_httpClientMock.Object,_optionsMock.Object);
      }

      [Fact]
      public async Task GetExchangeRate_ShouldReturnExchangeRatesResponse_WhenApiCallIsSuccessful()
      {
         // Arrange
         var currency = Currency.ALL;
         var expectedResponse = new ExchangeRatesResponse
         {
            rates = new Rates { EUR = 0.85 }
         };

         var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
         {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
         };

         _httpMessageHandlerMock.Protected()
             .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.IsAny<HttpRequestMessage>(),
                 ItExpr.IsAny<CancellationToken>())
             .ReturnsAsync(responseMessage);

         // Act
         var result = await _exchangeRateService.GetExchangeRate(currency);

         // Assert
         Assert.NotNull(result);
         Assert.Equal(expectedResponse.rates.EUR,result.rates.EUR);
      }

      [Fact]
      public async Task GetExchangeRate_ShouldThrowException_WhenApiCallFails()
      {
         // Arrange
         var currency = Currency.GBP;
         var responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);

         _httpMessageHandlerMock.Protected()
             .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.IsAny<HttpRequestMessage>(),
                 ItExpr.IsAny<CancellationToken>())
             .ReturnsAsync(responseMessage);

         // Act & Assert
         await Assert.ThrowsAsync<HttpRequestException>(() => _exchangeRateService.GetExchangeRate(currency));
      }
   }

}
