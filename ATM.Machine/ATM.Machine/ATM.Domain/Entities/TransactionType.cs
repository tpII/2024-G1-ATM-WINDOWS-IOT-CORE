using System;
using System.Text.Json.Serialization;

namespace ATM.Domain.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionType
{
    Withdraw, // Retiro
    Deposit,    // Depósito
    Transfer    // Transferencia
}
