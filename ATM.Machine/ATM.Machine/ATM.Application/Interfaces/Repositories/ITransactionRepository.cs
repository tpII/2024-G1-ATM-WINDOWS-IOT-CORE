using System;

namespace ATM.Application.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetByAccountIdAsync(string accountId);
    Task AddAsync(Transaction transaction); // Transferencias, retiros o depósitos
}