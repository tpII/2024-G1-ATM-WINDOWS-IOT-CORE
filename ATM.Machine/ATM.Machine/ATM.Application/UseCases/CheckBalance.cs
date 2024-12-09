using ATM.Application.Services;
using ATM.Application.Interfaces.Repositories;

namespace ATM.Application.UseCases
{
    public class CheckBalance
    {
        private readonly CheckBalanceService _checkBalanceService;

        public CheckBalance(CheckBalanceService checkBalanceService)
        {
            _checkBalanceService = checkBalanceService;
        }

        public async Task<decimal> ExecuteAsync(string accountId)
        {
            return await _checkBalanceService.CheckBalanceAsync(accountId); // Llama al servicio
        }
    }
}
