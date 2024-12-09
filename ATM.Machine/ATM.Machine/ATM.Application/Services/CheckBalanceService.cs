using ATM.Application.Interfaces.Repositories;
using ATM.Domain.Entities;

namespace ATM.Application.Services
{
    public class CheckBalanceService
    {
        private readonly IAccountRepository _accountRepository;

        public CheckBalanceService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<decimal> CheckBalanceAsync(string accountId)
        {
            if (!await _accountRepository.ExistsAsync(accountId))
                throw new Exception("Account does not exist.");

            return await _accountRepository.GetBalanceAsync(accountId); // Llama al repositorio para hacer la consulta HTTP
        }
    }
}
