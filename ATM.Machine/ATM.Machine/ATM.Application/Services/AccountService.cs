using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Services;
using ATM.Domain.Entities;
using System.Threading.Tasks;

namespace ATM.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<decimal> GetBalanceAsync(string accountId)
        {
            return await _accountRepository.GetBalanceAsync(accountId);
        }
        
        public async Task DepositAsync(string accountId, decimal amount)
        {
            // Crear la transacción de depósito
            var transaction = new Transaction(accountId, amount, TransactionType.Deposit,
                TransactionStatus.Pending, DateTime.UtcNow, "Deposito");

            // Enviar la solicitud al servidor para registrar la transacción
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task WithdrawAsync(string accountId, decimal amount)
        {
            // Crear la transacción de retiro
            var transaction = new Transaction(accountId, amount, TransactionType.Withdraw,
                TransactionStatus.Pending, DateTime.UtcNow, "Retiro en efectivo");

            // Enviar la solicitud al servidor para registrar la transacción
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task TransferAsync(string sourceAccountId, string destinationCbu, decimal amount)
        {
            // Crear la transacción de transferencia
            var transaction = new Transaction(sourceAccountId,  destinationCbu, 
                    amount, TransactionType.Transfer, TransactionStatus.Pending, 
                    DateTime.UtcNow, "Transferencia entre cuentas");

            // Enviar la solicitud al servidor para registrar la transacción
            await _transactionRepository.AddAsync(transaction);
        }
    }
}
