using System;

namespace ATM.Domain.Entities;

public class Client
{
    public string Id { get; set; } // Identificador único del cliente
    public string FullName { get; set; } // Nombre completo
    public string Email { get; set; } // Correo electrónico
    public string PhoneNumber { get; set; } // Número de teléfono
    public DateTime CreatedAt { get; set; } // Fecha de registro

    public Client(string id, string fullName, string email, string phoneNumber, DateTime createdAt)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        CreatedAt = createdAt;
    }
}