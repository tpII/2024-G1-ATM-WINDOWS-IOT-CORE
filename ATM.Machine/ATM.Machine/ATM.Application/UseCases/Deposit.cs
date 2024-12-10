using ATM.Application.Interfaces.Services;
using ATM.Application.Services;
using ATM.Domain.Entities;

namespace ATM.Application.UseCases
{
    public class Deposit
    {
        private readonly IAccountService _accountService;
        private readonly SessionService _sessionService;

        public Deposit(IAccountService accountService, SessionService sessionService)
        {
            _accountService = accountService;
            _sessionService = sessionService;
        }

        public async Task ExecuteAsync(decimal amount)
        {
            Console.WriteLine($"In use case: {amount}");
            // Realiza el depósito a través del servicio AccountService
            await _accountService.DepositAsync(_sessionService.GetAccountId(), amount);
        }
    }
}
