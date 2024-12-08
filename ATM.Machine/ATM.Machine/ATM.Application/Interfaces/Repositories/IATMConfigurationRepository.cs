using System;
using ATM.Domain.Entities;

namespace ATM.Application.Interfaces.Repositories;

public interface IATMConfigurationRepository
{
    void SetWithdrawalLimit(int limit);
    int GetWithdrawalLimit();
}