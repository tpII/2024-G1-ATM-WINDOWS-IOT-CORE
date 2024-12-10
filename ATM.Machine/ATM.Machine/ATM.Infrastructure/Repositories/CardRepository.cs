using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Domain.Entities;

namespace ATM.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly HttpClient _httpClient;

    public CardRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var response = await _httpClient.GetAsync($"api/cards/exists/{id}");

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> VerifyPinAsync(string cardId, string pin)
    {
        var jsonPayload = JsonSerializer.Serialize(new { cardId = cardId, pin = pin });
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/cards/verify-pin", content);

        // Si la respuesta es exitosa, devolvemos true. Si no, false.
        return response.IsSuccessStatusCode;
    }

}
    