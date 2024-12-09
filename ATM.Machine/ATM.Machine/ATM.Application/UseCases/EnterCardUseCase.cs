using System;
using ATM.Application.Services;

namespace ATM.Application.UseCases;

public class EnterCardUseCase
{
    private readonly SessionService _sessionService;

    public EnterCardUseCase(SessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task<bool> ExecuteAsync(string id)
    {
        var cardExists = await _sessionService.VerifyCardAsync(id);
        return cardExists;
    }
}