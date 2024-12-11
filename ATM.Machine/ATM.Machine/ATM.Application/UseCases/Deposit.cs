using ATM.Application.Interfaces.Services;
using ATM.Application.Services;
using ATM.Domain.Entities;

namespace ATM.Application.UseCases
{
    public class Deposit
    {
        private readonly IAccountService _accountService;
        private readonly SessionService _sessionService;
        private readonly CashManagementService _cashManager;

        public Deposit(IAccountService accountService, SessionService sessionService, CashManagementService cashManager)
        {
            _accountService = accountService;
            _sessionService = sessionService;
            _cashManager = cashManager;
        }

        public async Task ExecuteAsync(decimal amount)
        {
            Console.WriteLine($"In use case: {amount}");
            // Realiza el depósito a través del servicio AccountService
            try
            {
                await _accountService.DepositAsync(_sessionService.GetAccountId(), amount);
                await _cashManager.LoadCashAsync(amount);
            }
            catch (Exception e)
            {    
                throw new Exception(e.Message);
            }
        }
    }
}
