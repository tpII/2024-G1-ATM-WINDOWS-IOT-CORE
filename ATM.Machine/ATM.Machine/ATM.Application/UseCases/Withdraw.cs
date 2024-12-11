using ATM.Application.Interfaces.Services;
using ATM.Application.Services;

namespace ATM.Application.UseCases
{
    public class Withdraw
    {
        private readonly IAccountService _accountService;
        private readonly SessionService _sessionService;
        private readonly CashManagementService _cashManager;
        private readonly ATMConfigurationService _atmConfig;

        public Withdraw(IAccountService accountService, SessionService sessionService, CashManagementService cashManager, ATMConfigurationService atmConfig)
        {
            _accountService = accountService;
            _sessionService = sessionService;
            _cashManager = cashManager;
            _atmConfig = atmConfig;
        }

        public async Task ExecuteAsync(decimal amount)
        {
            // Realiza el retiro a través del servicio AccountService
            if(_atmConfig.MaxLimit < amount)
            {
                throw new Exception("El límite máximo de extracción es de $" + _atmConfig.MaxLimit);
            }
            else if(_atmConfig.MinLimit > amount)
            {
                throw new Exception("El límite mínimo de extracción es de $" + _atmConfig.MinLimit);
            }
            if(_cashManager.AvailableCash < amount)
            {
                throw new Exception("No hay suficiente dinero en el cajero");
            }
            await _cashManager.DispenseCashAsync(amount);
            await _accountService.WithdrawAsync(_sessionService.GetAccountId(), amount);
        }
    }
}
