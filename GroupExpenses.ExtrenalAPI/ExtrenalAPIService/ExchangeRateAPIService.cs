using GroupExpenses.Config;
using GroupExpenses.Enums;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GroupExpenses.APIGatway
{
   public class ExchangeRateAPIService
   {
      private readonly HttpClient _httpClient;
      private readonly ExternalApiSettings _apiSettings;

      public ExchangeRateAPIService(HttpClient httpClient,IOptions<ExternalApiSettings> apiSettings)
      {
         _httpClient = httpClient;
         _apiSettings = apiSettings.Value;
      }

      public async Task<ExchangeRatesResponse> GetExchangeRate(Currency currency)
      {
         var url = $"{_apiSettings.ExchangeRateUrl}{_apiSettings.Endpoint}{Enum.GetName(currency)}";
         var response = await _httpClient.GetAsync(url);
         response.EnsureSuccessStatusCode();

         var responseBody = await response.Content.ReadAsStringAsync();
         return JsonConvert.DeserializeObject<ExchangeRatesResponse>(responseBody);
            
         
      }
   }
}
