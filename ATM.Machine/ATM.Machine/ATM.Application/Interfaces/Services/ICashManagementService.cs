using System;

namespace ATM.Application.Interfaces.Services;

public interface ICashManagementService
{
    int GetAvailableCash();
    void LoadCash(int amount);
}