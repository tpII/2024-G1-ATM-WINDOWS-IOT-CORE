using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Device.Spi;
using System.Text;
using System.Threading;
using Iot.Device.Mfrc522;
using Iot.Device.Ndef;
using Iot.Device.Rfid;
using ATM.Application.Interfaces;

namespace ATM.Infrastructure.Services;

public class Windows10RfidReader : IRfidReader
{
    private MfRc522 MfRc522 { get; set; }
    private bool _disposed;

    public Windows10RfidReader()
    {
        try
        {
            GpioController gpioController = new GpioController(PinNumberingScheme.Logical, new Windows10Driver());
            int pinReset = 25;
            SpiConnectionSettings connection = new(0, 0)
            {
                ClockFrequency = 5_000_000
            };
            SpiDevice spi = SpiDevice.Create(connection);
            
            // Inicializar MfRc522
            MfRc522 = new(spi, pinReset, gpioController, true);
        }
        catch (Exception ex)
        {
            // Manejo de errores de inicialización
            throw new InvalidOperationException("Failed to initialize Windows10RfidReader.", ex);
        }
    }

    public async Task<byte[]> ReadCardIdAsync(CancellationToken cancellationToken)
    {
        bool res;
        Data106kbpsTypeA card;

        do
        {
            // Revisar si el token ha solicitado cancelación
            cancellationToken.ThrowIfCancellationRequested();

            // Intentar escuchar una tarjeta
            res = MfRc522.ListenToCardIso14443TypeA(out card, TimeSpan.FromSeconds(2));

            // Si no se detecta una tarjeta, esperar un poco
            if (!res)
            {
                await Task.Delay(200, cancellationToken); // Respetar el token durante la espera
            }

        } while (!res);

        // Retornar el NfcId de la tarjeta detectada
        return card.NfcId;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Liberar recursos manejados
                MfRc522.Dispose();
            }

            _disposed = true;
        }
    }

    ~Windows10RfidReader()
    {
        MfRc522.Dispose();
        Dispose(false);
    }
}