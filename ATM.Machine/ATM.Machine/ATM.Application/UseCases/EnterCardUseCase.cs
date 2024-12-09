using System;
using ATM.Application.Interfaces.Services;

namespace ATM.Application.UseCases;

public class EnterCardUseCase
{
    private readonly ICardService _cardService;

    public EnterCardUseCase(ICardService cardService)
    {
        _cardService = cardService;
    }

    public async Task<bool> ExecuteAsync(string id)
    {
        var cardExists = await _cardService.AuthenticateCardAsync(id);
        return cardExists;
    }
}