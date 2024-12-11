using System;
using ATM.Domain.Entities;

namespace ATM.Infrastructure.DTOs;

public class AddTransactionRequest
{
    public string AccountId { get; set; }

    public decimal Amount { get; set; }

    public string? DestinationCbu { get; set; }

    public TransactionType Type { get; set; }

    public string? Description { get; set; }

    public AddTransactionRequest(string accountId, decimal amount, string? destinationCbu, TransactionType type, string? description)
    {
        AccountId = accountId;
        Amount = amount;
        DestinationCbu = destinationCbu;
        Type = type;
        Description = description;
    }
}
