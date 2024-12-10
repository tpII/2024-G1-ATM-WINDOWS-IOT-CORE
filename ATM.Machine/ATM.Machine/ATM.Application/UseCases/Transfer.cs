using ATM.Application.Interfaces.Services;
using ATM.Application.Services;

namespace ATM.Application.UseCases
{
    public class Transfer
    {
        private readonly IAccountService _accountService;

        private readonly SessionService _sessionService;

        public Transfer(IAccountService accountService, SessionService sessionService)
        {
            _accountService = accountService;
            _sessionService = sessionService;
        }

        public async Task ExecuteAsync(string destinationCbu, decimal amount)
        {
            // Realiza la transferencia a través del servicio AccountService
            await _accountService.TransferAsync(_sessionService.GetAccountId(), destinationCbu, amount);
        }
    }
}
