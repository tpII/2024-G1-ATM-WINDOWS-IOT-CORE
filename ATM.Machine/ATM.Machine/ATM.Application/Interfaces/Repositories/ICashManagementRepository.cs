using System;
using ATM.Domain.Entities;

namespace ATM.Application.Interfaces.Repositories;

public interface ICashManagementRepository
{
    int GetAvailableCash();
    void LoadCash(int amount);
    int DispenseCash(int amount);
}