using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ATM.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using ATM.Infrastructure.Hubs;
using ATM.Application.UseCases;

namespace ATM.Infrastructure.Services;

public class RfidBackgroundService : BackgroundService
{
    private readonly IHubContext<CardNotificationHub> _hubContext;
    private readonly IRfidReader _rfidReader;
    private readonly EnterCardUseCase _useCase;

    public RfidBackgroundService(IHubContext<CardNotificationHub> hubContext, IRfidReader rfidReader, EnterCardUseCase useCase)
    {
        _hubContext = hubContext;
        _rfidReader = rfidReader;
        _useCase = useCase;
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
                        bool isValid = await _useCase.ExecuteAsync(cardId);
                        await _hubContext.Clients.All.SendAsync("CardDetected", isValid, stoppingToken);
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
        _rfidReader?.Dispose();
        base.Dispose();
    }

}

