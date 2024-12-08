using System;
using System.Net.Http;
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
            var response = await _httpClient.GetAsync($"api/accounts/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Account>(content);
        }

        public async Task<decimal> GetBalanceAsync(string accountId)
        {
            var response = await _httpClient.GetAsync($"api/accounts/{accountId}/balance");
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to retrieve balance.");

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<decimal>(content);
        }

        public async Task<bool> ExistsAsync(string accountId)
        {
            var response = await _httpClient.GetAsync($"api/accounts/{accountId}/exists");
            return response.IsSuccessStatusCode;

        }
    }
}
