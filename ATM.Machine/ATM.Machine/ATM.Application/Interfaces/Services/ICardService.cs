using System;

namespace ATM.Application.Interfaces.Services;

public interface ICardService
{
    // Autentica la tarjeta
    Task<bool> AuthenticateCardAsync(string cardId);
    
    // Lee el ID de la tarjeta RFID
    // Task<byte[]> ReadCardIdAsync();
    
    // Verifica el PIN de una tarjeta
    Task<bool> VerifyPinAsync(string cardId, string pin);
}