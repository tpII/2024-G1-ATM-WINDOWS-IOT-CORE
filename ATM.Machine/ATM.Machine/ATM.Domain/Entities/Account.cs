using System;

namespace ATM.Domain.Entities;

public class Account
{
    public string Id { get; set; } // Identificador único de la cuenta
    public string CBU { get; set; }
    public string AccountNumber { get; set; } // Número de cuenta
    public decimal Balance { get; set; } // Saldo actual
    public string ClientId { get; set; } // Relación con el cliente
    public DateTime CreatedAt { get; set; } // Fecha de creación
    public DateTime? UpdatedAt { get; set; } // Fecha de última actualización (opcional)

    public Account(string id, string cbu, string accountNumber, decimal balance, string clientId, DateTime createdAt)
    {
        Id = id;
        CBU = cbu;
        AccountNumber = accountNumber;
        Balance = balance;
        ClientId = clientId;
        CreatedAt = createdAt;
    }
}

