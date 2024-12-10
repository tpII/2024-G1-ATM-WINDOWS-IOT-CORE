using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;

namespace ATM.Infrastructure.Repositories
{
    public class ATMConfigurationRepository : IATMConfigurationRepository
    {
        private readonly HttpClient _httpClient;

        public ATMConfigurationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SetWithdrawalLimitsAsync(int maxLimit, int minLimit)
        {
            var requestBody = new { MaxLimit = maxLimit, MinLimit = minLimit };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/atm/configuration/set-limits", content);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to set withdrawal limits.");
        }

        public async Task<(int MaxLimit, int MinLimit)> GetWithdrawalLimitsAsync()
        {
            var response = await _httpClient.GetAsync("api/atm/configuration/get-limits");
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to retrieve withdrawal limits.");

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<(int MaxLimit, int MinLimit)>(responseContent);

            return result;
        }
    }
}
