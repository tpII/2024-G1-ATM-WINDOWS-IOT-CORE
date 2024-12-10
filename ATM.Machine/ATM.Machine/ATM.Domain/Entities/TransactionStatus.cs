using System;
using System.Text.Json.Serialization;

namespace ATM.Domain.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionStatus
{
    Pending,    // En espera
    Completed,  // Completada
    Failed      // Fallida
}
