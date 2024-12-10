using System;

namespace ATM.Domain.Entities;
public class Transaction
{   
    public string Id { get; set; } // Identificador único de la transacción
    public string AccountId { get; set; } // ID de la cuenta que realiza la transacción
    public string? DestinationCbu { get; set; } // CBU de la cuenta receptora (si es una transferencia)
    public decimal Amount { get; set; } // Monto de la transacción
    public TransactionType Type { get; set; } // Tipo de transacción (Depósito, Retiro, Transferencia)
    public TransactionStatus? Status { get; set; } // Estado (completada, fallida, pendiente)
    public DateTime? CreatedAt { get; set; } // Fecha y hora de la transacción
    public string? Description { get; set; } // Detalles adicionales de la transacción

    public Transaction(string accountId, decimal amount, TransactionType type, TransactionStatus status, DateTime createdAt, string? description)
    {
        Id = "";
        AccountId = accountId;
        Amount = amount;
        Type = type;
        Status = status;
        CreatedAt = createdAt;
        Description = description;
    }

    public Transaction(string accountId,string destinationCbu, decimal amount, TransactionType type, TransactionStatus status, DateTime createdAt, string? description)
    {
        Id = "";
        AccountId = accountId;
        DestinationCbu = destinationCbu;
        Amount = amount;
        Type = type;
        Status = status;
        CreatedAt = createdAt;
        Description = description;
    }
}