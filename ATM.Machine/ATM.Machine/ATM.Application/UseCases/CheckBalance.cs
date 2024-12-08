using System;
using ATM.Application.Interfaces.Repositories;

namespace ATM.Application.UseCases;

public class CheckBalance
{
    private readonly IAccountRepository _accountRepository;

    public CheckBalance(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<decimal> ExecuteAsync(string accountId)
    {
        if (!await _accountRepository.ExistsAsync(accountId))
            throw new Exception("Account does not exist.");

        return await _accountRepository.GetBalanceAsync(accountId);
    }

}
