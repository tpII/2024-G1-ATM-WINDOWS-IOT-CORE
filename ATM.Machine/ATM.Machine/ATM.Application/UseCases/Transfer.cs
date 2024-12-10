using ATM.Application.Interfaces.Services;

namespace ATM.Application.UseCases
{
    public class Transfer
    {
        private readonly IAccountService _accountService;

        public Transfer(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task ExecuteAsync(string sourceAccountId, string destinationAccountId, decimal amount)
        {
            // Realiza la transferencia a través del servicio AccountService
            await _accountService.TransferAsync(sourceAccountId, destinationAccountId, amount);
        }
    }
}
