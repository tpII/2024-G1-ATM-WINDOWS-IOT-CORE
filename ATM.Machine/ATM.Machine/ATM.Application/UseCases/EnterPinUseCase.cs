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

    public async Task<bool> ExecuteAsync(string pin)
    {
        return await _sessionService.VerifyPinAsync(pin);
    }
}
