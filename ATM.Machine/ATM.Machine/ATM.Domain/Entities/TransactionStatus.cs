using System;

namespace ATM.Domain.Entities;

public enum TransactionStatus
{
    Pending,    // En espera
    Completed,  // Completada
    Failed      // Fallida
}
