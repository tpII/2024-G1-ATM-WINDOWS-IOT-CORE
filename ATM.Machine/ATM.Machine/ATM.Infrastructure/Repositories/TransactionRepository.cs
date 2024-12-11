using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Exceptions;
using ATM.Domain.Entities;
using ATM.Infrastructure.Utils;
using ATM.Infrastructure.DTOs;

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

        var responseContent = await response.Content.ReadAsStringAsync();

        var payload = responseContent.Deserialize<IEnumerable<Transaction>>();

        // Verificar si la respuesta es exitosa
        if (!response.IsSuccessStatusCode || payload == null)
            throw new RepositoryException("Ocurrio un error al recuperar las transacciones. Intentelo nuevamente");
        
        return payload;
    }

    // Método para agregar una nueva transacción
    public async Task AddAsync(Transaction transaction)
    {
        var data = new AddTransactionRequest(transaction.AccountId, transaction.Amount, transaction.DestinationCbu, transaction.Type, transaction.Description);

        // Serializar la transacción a JSON
        
        var content = new StringContent(CustomJsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        var payload = new AddTransactionResponse();
        HttpResponseMessage? response = null;
        try
        {
            response = await _httpClient.PostAsync("api/transactions/", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            payload = responseContent?.Deserialize<AddTransactionResponse>();
            if(!response.IsSuccessStatusCode)
            {
                throw new RepositoryException(payload?.Message ?? "Ocurrió un error con su transacción. Inténtelo nuevamente");
            }
        }
        catch (Exception)
        {
            throw new RepositoryException(payload?.Message ?? "Ocurrió un error con su transacción. Inténtelo nuevamente");
        }
    }
}