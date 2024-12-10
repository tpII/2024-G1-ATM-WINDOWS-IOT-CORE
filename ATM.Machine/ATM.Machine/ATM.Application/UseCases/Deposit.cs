using ATM.Application.Interfaces.Services;
using ATM.Domain.Entities;

namespace ATM.Application.UseCases
{
    public class Deposit
    {
        private readonly IAccountService _accountService;

        public Deposit(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task ExecuteAsync(string accountId, decimal amount)
        {
            // Realiza el depósito a través del servicio AccountService
            await _accountService.DepositAsync(accountId, amount);
        }
    }
}
