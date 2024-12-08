using System;
using ATM.Domain.Entities;
namespace ATM.Application.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(string id);
    Task<decimal> GetBalanceAsync(string accountId);
    Task<bool> ExistsAsync(string accountId);
}
