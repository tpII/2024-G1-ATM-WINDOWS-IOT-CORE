using System;
using ATM.Application.Interfaces.Services;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Security;

namespace ATM.Application.Services;

public class SessionService : ISessionService
{
    public string? CardId { get; set; }

    private readonly ICardRepository _cardRepository;

    private readonly IEncryptionService _encryptionService;

    public SessionService(ICardRepository cardRepository, IEncryptionService encryptionService)
    {
        _cardRepository = cardRepository;
        _encryptionService = encryptionService;
    }

    public async Task<bool> VerifyCardAsync(string id)
    {
        CardId = id;
        return await _cardRepository.ExistsAsync(id);
    }

    public async Task<bool> VerifyPinAsync(int pin)
    {
        if(CardId == null)
        {
            throw new Exception("Servicio de sesión: Antes de verificar el pin se debe autenticar una tarjeta para la sesion");
        }
        string pinHash = _encryptionService.Hash($"{pin}");
        return await _cardRepository.VerifyPinAsync(CardId, pinHash);
    }
}
