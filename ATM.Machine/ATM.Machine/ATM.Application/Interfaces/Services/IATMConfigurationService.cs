using System;

namespace ATM.Application.Interfaces.Services;

public interface IATMConfigurationService
{
    void SetWithdrawalLimit(int limit);
    int GetWithdrawalLimit();
}
