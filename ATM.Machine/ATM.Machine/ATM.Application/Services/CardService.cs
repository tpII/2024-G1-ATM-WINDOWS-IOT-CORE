using System;
using ATM.Application.Interfaces.Services;
using ATM.Application.Interfaces.Repositories;

namespace ATM.Application.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;

    public CardService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<bool> AuthenticateCardAsync(string id)
    {
        return await _cardRepository.ExistsAsync(id);
    }

    // public async Task<byte[]> ReadCardIdAsync()
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<bool> VerifyPinAsync(string cardId, string pin)
    {
        return await _cardRepository.VerifyPinAsync(cardId, pin);
    }
}
