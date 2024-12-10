using System;
using ATM.Application.Interfaces.Services;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Security;

namespace ATM.Application.Services;

public class SessionService : ISessionService
{
    public string? CardId { get; set; }
    public string? AccountId { get; set; }

    private readonly ICardRepository _cardRepository;

    private readonly IEncryptionService _encryptionService;

    public SessionService(ICardRepository cardRepository, IEncryptionService encryptionService)
    {
        _cardRepository = cardRepository;
        _encryptionService = encryptionService;
    }

    public async Task<bool> VerifyCardAsync(string number)
    {
        CardId = await _cardRepository.ExistsAsync(number);
        return CardId != null;
    }

    public async Task<bool> VerifyPinAsync(string pin)
    {
        string id = GetCardId();
        string pinHash = _encryptionService.Hash(pin);
        AccountId = await _cardRepository.VerifyPinAsync(id, pinHash);
        return AccountId != null;
    }

    public string GetCardId()
    {
        if(CardId == null)
        {
            throw new Exception("Servicio de sesión: no existe una sesion actualmente");
        }
        return CardId;
    }

    public string GetAccountId()
    {
        if(AccountId == null)
        {
            throw new Exception("Servicio de sesión: no existe una sesion actualmente");
        }
        return AccountId;
    }

    public void LogOut()
    {
        CardId = null;
        AccountId = null;
    }
}
