using System;

namespace ATM.Domain.Entities;

public class Transaction
{   
    public string? Id { get; set; } // Identificador único de la transacción
    public string AccountId { get; set; } // ID de la cuenta que realiza la transacción
    public string CardId { get; set; } // Relación con la tarjeta usada
    public string? DestinationAccountId { get; set; } // ID de la cuenta receptora (si es una transferencia)
    public decimal Amount { get; set; } // Monto de la transacción
    public TransactionType Type { get; set; } // Tipo de transacción (Depósito, Retiro, Transferencia)
    public TransactionStatus? Status { get; set; } // Estado (completada, fallida, pendiente)
    public DateTime? CreatedAt { get; set; } // Fecha y hora de la transacción
    public string? Description { get; set; } // Detalles adicionales de la transacción

    public Transaction(string accountId, string cardId, decimal amount, TransactionType type, TransactionStatus status, DateTime createdAt, string? description)
    {
        AccountId = accountId;
        CardId = cardId;
        Amount = amount;
        Type = type;
        Status = status;
        CreatedAt = createdAt;
        Description = description;
    }

    public Transaction(string accountId, string cardId, string destinationAccountId, decimal amount, TransactionType type, TransactionStatus status, DateTime createdAt, string? description)
    {
        AccountId = accountId;
        CardId = cardId;
        DestinationAccountId = destinationAccountId;
        Amount = amount;
        Type = type;
        Status = status;
        CreatedAt = createdAt;
        Description = description;
    }
}