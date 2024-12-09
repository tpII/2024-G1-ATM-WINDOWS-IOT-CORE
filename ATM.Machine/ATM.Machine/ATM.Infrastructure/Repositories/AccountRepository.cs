using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Domain.Entities;

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
            var requestBody = new { id = id};

            // Serializar a JSON
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Enviar la solicitud POST
            var response = await _httpClient.PostAsync("api/accounts/get-by-id", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Account>(responseContent);
        }

        public async Task<decimal> GetBalanceAsync(string accountId)
        {
            // Crear el objeto del cuerpo de la solicitud
            var requestBody = new { accountId = accountId};

            // Serializar a JSON
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Enviar la solicitud POST
            var response = await _httpClient.PostAsync("api/accounts/check-balance", content);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to retrieve balance.");

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetBalanceResponse>(responseContent).balance;
        }

        public async Task<bool> ExistsAsync(string accountId)
        {
            // Crear el objeto del cuerpo de la solicitud
            var requestBody = new { accountId = accountId };

            // Serializar a JSON
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Enviar la solicitud POST
            var response = await _httpClient.PostAsync("api/accounts/exists", content);

            if (!response.IsSuccessStatusCode)
                return false;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(responseContent);
        }
    }

    public class GetBalanceResponse{
        public decimal balance {get; set;}
        public bool Success {get; set;}
    }
}
