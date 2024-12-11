using System;

namespace ATM.Infrastructure.DTOs;

public class AddTransactionResponse
{
    public bool? Success {get; set;}
    public string? Message {get; set;}
}
