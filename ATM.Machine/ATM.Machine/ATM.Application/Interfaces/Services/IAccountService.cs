using System;

namespace ATM.Application.Interfaces.Services;

public interface IAccountService
{
    Task<decimal> GetBalanceAsync(string accountId);
    Task DepositAsync(string accountId, decimal amount);
    Task WithdrawAsync(string accountId, decimal amount);
    Task TransferAsync(string sourceAccountId, string destinationCbu, decimal amount);
}