using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Domain.Entities;
using ATM.Infrastructure.Utils;

namespace ATM.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly HttpClient _httpClient;

    public CardRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> ExistsAsync(string number)
    {
        var response = await _httpClient.GetAsync($"api/cards/exists/{number}");
        var content = await response.Content.ReadAsStringAsync();
        var payload = content.Deserialize<ExistsResponse>();
        return payload.CardId;
    }

    public async Task<string?> VerifyPinAsync(string cardId, string pin)
    {
        var jsonPayload = CustomJsonSerializer.Serialize(new { CardId = cardId, Pin = pin });
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/cards/verify-pin", content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var payload = responseContent.Deserialize<VerifyResponse>();
        return payload.AccountId;
    }
}

public class ExistsResponse
{
    public string? CardId { get; set; }
}

public class VerifyResponse
{
    public string? AccountId { get; set; }
}