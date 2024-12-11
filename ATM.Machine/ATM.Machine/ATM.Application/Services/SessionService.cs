using System;
using ATM.Application.Interfaces.Services;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Security;
using ATM.Application.Exceptions;

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
        CardId = await _cardRepository.GetIdByNumber(number);
        return CardId != null;
    }

    public async Task<bool> VerifyPinAsync(string pin)
    {
        string id = GetCardId();
        string pinHash = _encryptionService.Hash(pin);
        AccountId = await _cardRepository.VerifyPinAsync(id, pinHash);
        return AccountId != null;
    }

    public bool IsSessionActive()
    {
        return (CardId == null || AccountId == null);
    }

    public string GetCardId()
    {
        if(CardId == null)
        {
            throw new SessionException("No se ha iniciado una sesión previamente");
        }
        return CardId;
    }

    public string GetAccountId()
    {
        if(AccountId == null)
        {
            throw new SessionException("No se ha iniciado una sesión previamente");
        }
        return AccountId;
    }

    public void LogOut()
    {
        CardId = null;
        AccountId = null;
    }
}
