using ATM.Application.Interfaces.Services;

namespace ATM.Application.UseCases
{
    public class CheckBalance
    {
        private readonly IAccountService _accountService;

        public CheckBalance(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<decimal> ExecuteAsync(string accountId)
        {
            return await _accountService.GetBalanceAsync(accountId);
        }
    }
}
