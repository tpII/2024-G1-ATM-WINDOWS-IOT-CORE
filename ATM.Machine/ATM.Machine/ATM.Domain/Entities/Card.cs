using System;

namespace ATM.Domain.Entities;

public class Card
{
    public string Id { get; set; } // Identificador único de la tarjeta
    public string Number { get; set; } // Número de tarjeta
    public string AccountId { get; set; } // Relación con la cuenta
    public string Pin { get; set; } // Hash del PIN de la tarjeta
    public bool IsActive { get; set; } // Estado de la tarjeta
    public DateTime ExpirationDate { get; set; } // Fecha de expiración
    public DateTime CreatedAt { get; set; } // Fecha de emisión
    public DateTime? UpdatedAtAt { get; set; } // Fecha de emisión

    public Card(string id, string number, DateTime expirationDate, string accountId, bool isActive, DateTime createdAt, string pin)
    {
        Id = id;
        Number = number;
        ExpirationDate = expirationDate;
        AccountId = accountId;
        IsActive = isActive;
        CreatedAt = createdAt;
        Pin = pin;
    }
}