using ATM.Application.Interfaces.Services;

namespace ATM.Application.UseCases
{
    public class Withdrawal
    {
        private readonly IAccountService _accountService;

        public Withdrawal(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task ExecuteAsync(string accountId, decimal amount)
        {
            // Realiza el retiro a través del servicio AccountService
            await _accountService.WithdrawAsync(accountId, amount);
        }
    }
}
