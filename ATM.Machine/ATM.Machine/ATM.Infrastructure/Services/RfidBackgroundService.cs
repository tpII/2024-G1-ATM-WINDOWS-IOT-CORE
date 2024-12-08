using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ATM.Application.Interfaces;

namespace ATM.Infrastructure.Services;

public class RfidBackgroundService : BackgroundService
{
    private readonly HttpClient _httpClient;
    private IRfidReader _rfidReader;

    public RfidBackgroundService(HttpClient httpClient, IRfidReader rfidReader)
    {
        _httpClient = httpClient;
        _rfidReader = rfidReader;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                { 
                    Console.WriteLine("Place your Mifare or Ultralight card on the reader.");

                    byte[] uid = await _rfidReader.ReadCardIdAsync(stoppingToken);

                    string cardId = BitConverter.ToString(uid).Replace("-", "");

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
                catch (OperationCanceledException)
                {
                    // Se lanza esta excepción cuando el token solicita cancelación.
                    Console.WriteLine("Operation canceled.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while reading a card in RfidBackgroundService: {ex.Message}");
                }

                // Pausa para evitar llamadas excesivas
                await Task.Delay(1000, stoppingToken);
            }
        }
        finally
        {
            Console.WriteLine("RfidBackgroundService is stopping.");
        }
    }


    public override void Dispose()
    {
        _httpClient.Dispose();
        _rfidReader?.Dispose();
        base.Dispose();
    }

}

