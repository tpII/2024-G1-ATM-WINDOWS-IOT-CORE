using ATM.Application.Interfaces.Repositories;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ATM.Infrastructure.Repositories
{
    public class CashManagementRepository : ICashManagementRepository
    {
        private readonly HttpClient _httpClient;

        public CashManagementRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public int GetAvailableCash()
        {
            var response = _httpClient.GetAsync("api/cash-management/available-cash").Result;
            response.EnsureSuccessStatusCode();

            var responseContent = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<int>(responseContent);
        }

        public void LoadCash(int amount)
        {
            var content = new StringContent(JsonSerializer.Serialize(amount), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("api/cash-management/load-cash", content).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
