using ATM.Application.Interfaces.Services;
using ATM.Application.Services;

namespace ATM.Application.UseCases
{
    public class Withdraw
    {
        private readonly IAccountService _accountService;

        private readonly SessionService _sessionService;

        public Withdraw(IAccountService accountService, SessionService sessionService)
        {
            _accountService = accountService;
            _sessionService = sessionService;
        }

        public async Task ExecuteAsync(decimal amount)
        {
            // Realiza el retiro a través del servicio AccountService
            await _accountService.WithdrawAsync(_sessionService.GetAccountId(), amount);
        }
    }
}
