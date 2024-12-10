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

    public async Task<bool> ExecuteAsync(string number)
    {
        return await _sessionService.VerifyCardAsync(number);
    }
}