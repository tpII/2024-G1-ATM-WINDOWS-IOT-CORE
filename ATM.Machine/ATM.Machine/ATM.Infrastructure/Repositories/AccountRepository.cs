using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Domain.Entities;
using ATM.Infrastructure.Utils;

namespace ATM.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HttpClient _httpClient;

        public AccountRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Account?> GetByIdAsync(string id)
        {
            // Crear el objeto del cuerpo de la solicitud
            var requestBody = new { Id = id };

            // Serializar a JSON
            var content = new StringContent(CustomJsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Enviar la solicitud POST
            var response = await _httpClient.PostAsync("api/accounts/get-by-id", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Account>(responseContent);
        }

        public async Task<decimal> GetBalanceAsync(string accountId)
        {

            Console.WriteLine($"AccounId: {accountId}");

            // Enviar la solicitud POST
            var response = await _httpClient.GetAsync($"api/accounts/balance/{accountId}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to retrieve balance.");

            var responseContent = await response.Content.ReadAsStringAsync();
            var payload = responseContent.Deserialize<BalanceResponse>();
            return payload.Balance;
        }

        public async Task<bool> ExistsAsync(string accountId)
        {
            // Crear el objeto del cuerpo de la solicitud
            var requestBody = new { AccountId = accountId };

            // Serializar a JSON
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Enviar la solicitud POST
            var response = await _httpClient.PostAsync("api/accounts/exists", content);

            return response.IsSuccessStatusCode;
        }
    }
}

public class BalanceResponse
{
    public decimal Balance { get; set; }
}