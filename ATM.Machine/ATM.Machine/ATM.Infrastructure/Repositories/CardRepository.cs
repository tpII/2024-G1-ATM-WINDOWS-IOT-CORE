using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Exceptions;
using ATM.Domain.Entities;
using ATM.Infrastructure.Utils;
using ATM.Infrastructure.DTOs;

namespace ATM.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly HttpClient _httpClient;

    public CardRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> GetIdByNumber(string number)
    {
        var payload = new CardExistsResponse();
        try
        {
            var response = await _httpClient.GetAsync($"api/cards/exists/{number}");
            var content = await response.Content.ReadAsStringAsync();
            payload = content.Deserialize<CardExistsResponse>();
        }
        catch (Exception)
        {
            throw new RepositoryException("Ocurrió un error al verificar su tarjeta. Inténtelo nuevamente");
        }
        return payload?.CardId;
    }

    public async Task<string?> VerifyPinAsync(string cardId, string pin)
    {
        var jsonPayload = CustomJsonSerializer.Serialize(new VerifyPinRequest(cardId, pin));
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        
        var payload = new VerifyPinResponse();
        HttpResponseMessage? response = null;
        try
        {
            response = await _httpClient.PostAsync("api/cards/verify-pin", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            payload = responseContent.Deserialize<VerifyPinResponse>();
        }
        catch (Exception)
        {
            throw new RepositoryException("Ocurrió un error al verificar su pin. Inténtelo nuevamente");
        }
        return payload?.AccountId;
    }
}