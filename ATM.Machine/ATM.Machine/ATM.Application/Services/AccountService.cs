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
            var transaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = accountId,
                Amount = amount,
                Type = TransactionType.Deposit,
                Status = TransactionStatus.Pending, // Estado inicial
                CreatedAt = DateTime.UtcNow,
                Description = "Depósito en efectivo"
            };

            // Enviar la solicitud al servidor para registrar la transacción
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task WithdrawAsync(string accountId, decimal amount)
        {
            // Crear la transacción de retiro
            var transaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = accountId,
                Amount = amount,
                Type = TransactionType.Withdrawal,
                Status = TransactionStatus.Pending, // Estado inicial
                CreatedAt = DateTime.UtcNow,
                Description = "Retiro en efectivo"
            };

            // Enviar la solicitud al servidor para registrar la transacción
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task TransferAsync(string sourceAccountId, string destinationAccountId, decimal amount)
        {
            // Crear la transacción de transferencia
            var transaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = sourceAccountId,
                DestinationAccountId = destinationAccountId,
                Amount = amount,
                Type = TransactionType.Transfer,
                Status = TransactionStatus.Pending, // Estado inicial
                CreatedAt = DateTime.UtcNow,
                Description = "Transferencia entre cuentas"
            };

            // Enviar la solicitud al servidor para registrar la transacción
            await _transactionRepository.AddAsync(transaction);
        }
    }
}
