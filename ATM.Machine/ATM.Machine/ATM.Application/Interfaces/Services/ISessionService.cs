using System;

namespace ATM.Application.Interfaces.Services;

public interface ISessionService
{
    // Autentica la tarjeta
    Task<bool> VerifyCardAsync(string cardId);
    
    // Verifica el PIN de una tarjeta
    Task<bool> VerifyPinAsync(string pin);
}