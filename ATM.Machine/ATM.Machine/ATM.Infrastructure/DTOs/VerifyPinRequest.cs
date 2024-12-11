using System;

namespace ATM.Infrastructure.DTOs;

public class VerifyPinRequest
{
    public string CardId { get; set; }

    public string Pin { get; set; }

    public VerifyPinRequest(string cardId, string pin)
    {
        CardId = cardId;
        Pin = pin;
    }
}
