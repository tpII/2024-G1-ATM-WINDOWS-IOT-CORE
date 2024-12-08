using System;

namespace ATM.Domain.Entities;

public class Card
{
    public string Id { get; set; } // Identificador único de la tarjeta
    public string CardNumber { get; set; } // Número de tarjeta
    public string CardHolderName { get; set; } // Nombre del titular
    public DateTime ExpirationDate { get; set; } // Fecha de expiración
    public string AccountId { get; set; } // Relación con la cuenta
    public bool IsActive { get; set; } // Estado de la tarjeta
    public DateTime CreatedAt { get; set; } // Fecha de emisión
    public string SecurityCodeHash { get; set; } // Hash del CVV (opcional por seguridad)
    public string PINHash { get; set; } // Hash del PIN de la tarjeta

    public Card(string id, string cardNumber, string cardHolderName, DateTime expirationDate, string accountId, bool isActive, DateTime createdAt, string securityCodeHash, string pinHash)
    {
        Id = id;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpirationDate = expirationDate;
        AccountId = accountId;
        IsActive = isActive;
        CreatedAt = createdAt;
        SecurityCodeHash = securityCodeHash;
        PINHash = pinHash;
    }
}