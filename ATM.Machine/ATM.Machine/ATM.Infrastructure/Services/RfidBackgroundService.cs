using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ATM.Infrastructure.Services
{
    public class RfidBackgroundService : BackgroundService
    {
        private readonly HttpClient _httpClient;

        public RfidBackgroundService()
        {
            _httpClient = new HttpClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Simulación: Leer tarjeta RFID
                    string cardId = await ReadCardIdAsync();

                    if (!string.IsNullOrEmpty(cardId))
                    {
                        // Crea el payload de la solicitud
                        var payload = new { CardId = cardId };
                        var jsonPayload = JsonSerializer.Serialize(payload);
                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                        // Enviar notificación al servidor
                        var response = await _httpClient.PostAsync("http://localhost:5000/api/cards/detected", content, stoppingToken);
                        
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Error notificando al servidor: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en RfidBackgroundService: {ex.Message}");
                }

                // Pausa para evitar llamadas excesivas
                await Task.Delay(500, stoppingToken);
            }
        }

        private Task<string> ReadCardIdAsync()
        {
            // Lógica de lectura bloqueante del lector RFID
            // Simulación: Leer un ID de tarjeta
            // return Task.FromResult("12345"); // Cambia por la lógica real
            return Task.FromResult<string>("12345"); // Cambia por la lógica real
        }

        public override void Dispose()
        {
            _httpClient.Dispose();
            base.Dispose();
        }
    }
}
