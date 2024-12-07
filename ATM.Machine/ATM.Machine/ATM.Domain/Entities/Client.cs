using System;

namespace ATM.Domain.Entities;

public class Client
{
    public string Id { get; set; } // Identificador único del cliente
    public string FullName { get; set; } // Nombre completo
    public string Email { get; set; } // Correo electrónico
    public string PhoneNumber { get; set; } // Número de teléfono
    public DateTime CreatedAt { get; set; } // Fecha de registro
}