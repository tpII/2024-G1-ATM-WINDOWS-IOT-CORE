using System;

namespace ATM.Application.Interfaces.Services;

public interface ICardService
{
    // Verifica el PIN de una tarjeta
    Task<bool> VerifyPinAsync(string cardId, string pin);

    // Lee el ID de la tarjeta RFID
    Task<string> ReadCardIdAsync();

    // Autentica la tarjeta
    Task<bool> AuthenticateCardAsync(string pin);
}