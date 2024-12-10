using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;

namespace ATM.Infrastructure.Repositories
{
    public class CashManagementRepository : ICashManagementRepository
    {
        private readonly HttpClient _httpClient;

        public CashManagementRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetAvailableCashAsync()
        {
            var response = await _httpClient.GetAsync("/api/atm/available-cash");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<int>(json);

            return result;
        }

        public async Task LoadCashAsync(int amount)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { amount }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/atm/load-cash", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
