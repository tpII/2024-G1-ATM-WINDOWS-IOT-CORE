using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Device.Spi;
using Iot.Device.Board;
using Iot.Device.Card.Mifare;
using Iot.Device.Card.Ultralight;
using Iot.Device.Mfrc522;
using Iot.Device.Ndef;
using Iot.Device.Rfid;

namespace ATM.Infrastructure.Services
{
    public class RfidBackgroundService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private MfRc522? _mfrc522;
        private bool isInit;

        public RfidBackgroundService()
        {
            _httpClient = new HttpClient();
            _mfrc522 = null;
            isInit = false;
        }

        public void Init()
        {
            Console.WriteLine("Hello MFRC522! Inicializando");
            GpioController gpioController = new GpioController(PinNumberingScheme.Logical, new Windows10Driver());
            int pinReset = 25;
            SpiConnectionSettings connection = new(0, 0);
            connection.ClockFrequency = 5_000_000;
            SpiDevice spi = SpiDevice.Create(connection);
            _mfrc522 = new(spi, pinReset, gpioController, true);
            isInit = true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                { 
                    if(!isInit)
                    {
                        Init();
                        Console.WriteLine("Fin de inicializacion");
                        if (_mfrc522 is not object)
                        {
                            Console.WriteLine("Something went wrong");
                            return;
                        }
                    }

                    Console.WriteLine($"Version: {_mfrc522?.Version}, version should be 1 or 2. Some clones may appear with version 0");
                    Console.WriteLine("Place your Mifare or Ultralight card on the reader.");
                    Console.WriteLine($"The default B key for Mifare ({BitConverter.ToString(MifareCard.DefaultKeyB.ToArray())}) will be used to read the card.");
                    Console.WriteLine($"The default password for Ultralight ({BitConverter.ToString(UltralightCard.DefaultPassword)}) will be used if write permissions require authentication.");

                    byte[] uid = await ReadCardIdAsync();

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
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en RfidBackgroundService: {ex.Message}");
                }

                // Pausa para evitar llamadas excesivas
                await Task.Delay(1000, stoppingToken);
            }
        }

        public byte[] ReadCardId()
        {
            if(_mfrc522 == null)
            {
                Console.WriteLine("ERROR: _mfrc522 es nulo");
                return new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            }
            bool res;
            Data106kbpsTypeA card;
            do
            {
                res = _mfrc522.ListenToCardIso14443TypeA(out card, TimeSpan.FromSeconds(2));
                Thread.Sleep(res ? 0 : 200);
            }
            while (!res);
            return card.NfcId;
        }

        public async Task<byte[]> ReadCardIdAsync()
        {
            if(_mfrc522 == null)
            {
                Console.WriteLine("ERROR: _mfrc522 es nulo");
                return new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            }
            bool res;
            Data106kbpsTypeA card;
            do
            {
                // Esperar de manera asincrónica en lugar de usar Thread.Sleep
                res = _mfrc522.ListenToCardIso14443TypeA(out card, TimeSpan.FromSeconds(2));

                // Esperar un poco si no se detecta la tarjeta, evitando bloquear el hilo
                if (!res)
                {
                    await Task.Delay(200);
                }

            } while (!res);

            // Retorna el NfcId de la tarjeta detectada
            return card.NfcId;
        }


        public override void Dispose()
        {
            _httpClient.Dispose();
            _mfrc522?.Dispose();
            base.Dispose();
        }

    }
}
