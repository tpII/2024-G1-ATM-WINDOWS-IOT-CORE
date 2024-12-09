using System;
using ATM.Application.Services;

namespace ATM.Application.UseCases;

public class EnterPinUseCase
{
    private readonly SessionService _sessionService;

    public EnterPinUseCase(SessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task<bool> ExecuteAsync(int pin)
    {
        var isCorrect = await _sessionService.VerifyPinAsync(pin);
        return isCorrect;
    }
}
