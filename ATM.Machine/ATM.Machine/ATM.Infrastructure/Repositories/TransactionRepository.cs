using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Domain.Entities;
using ATM.Infrastructure.Utils;

namespace ATM.Infrastructure.Repositories;
public class TransactionRepository : ITransactionRepository
{
    private readonly HttpClient _httpClient;

    public TransactionRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método para obtener todas las transacciones de una cuenta
    public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(string accountId)
    {
        // Crear el objeto del cuerpo de la solicitud
        var requestBody = new { AccountId = accountId };

        // Serializar el objeto a JSON
        var content = new StringContent(CustomJsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        // Enviar la solicitud POST al servidor Node.js
        var response = await _httpClient.PostAsync("api/transactions/get-by-account-id", content);

        // Verificar si la respuesta es exitosa
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Failed to retrieve transactions.");

        // Leer el contenido de la respuesta y deserializarlo
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent.Deserialize<IEnumerable<Transaction>>();
    }

    // Método para agregar una nueva transacción
    public async Task AddAsync(Transaction transaction)
    {
        var payload = new { 
            AccountId = transaction.AccountId,
            Amount = transaction.Amount,
            DestinationCbu = transaction.DestinationCbu,
            Type = transaction.Type,
            Description = transaction.Description,
        };
        // Serializar la transacción a JSON
        var content = new StringContent(CustomJsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        // Enviar la solicitud POST al servidor Node.js
        var response = await _httpClient.PostAsync("api/transactions/", content);

        // Verificar si la respuesta es exitosa
        if (!response.IsSuccessStatusCode) {
            var responseContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(responseContent.Deserialize<AddResponse>().Message);
        }
    }
}

public class AddResponse
{
    public bool? Success {get; set;}
    public string? Message {get; set;}
}