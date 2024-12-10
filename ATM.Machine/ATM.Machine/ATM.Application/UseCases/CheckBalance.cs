using ATM.Application.Interfaces.Services;
using ATM.Application.Services;

namespace ATM.Application.UseCases
{
    public class CheckBalance
    {
        private readonly IAccountService _accountService;

        private readonly SessionService _sessionService;

        public CheckBalance(IAccountService accountService, SessionService sessionService)
        {
            _accountService = accountService;
            _sessionService = sessionService;
        }

        public async Task<decimal> ExecuteAsync()
        {
            Console.WriteLine($"AccountId: {_sessionService.GetAccountId()}");
            return await _accountService.GetBalanceAsync(_sessionService.GetAccountId());
        }
    }
}
