using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ATM.Core.Services
{
    public class ApiService
    {
        public HttpClient Client { get; }

        public ApiService(HttpClient client)
        {
            Client = client;
        }

        public async Task<bool> DepositAsync(decimal amount)
        {
            var jsonContent = JsonSerializer.Serialize(new { amount = amount });

            // Crear el contenido HTTP con el JSON serializado
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST
            var response = await Client.PostAsync("http://localhost:5010/api/deposit", content);
            return response.IsSuccessStatusCode;
        }

        // Add more methods for other operations as needed
    }
}