using System;

namespace ATM.Application.Interfaces.Repositories;

public interface ICardRepository
{
    Task<Card?> GetByIdAsync(string id);
    Task<bool> VerifyPinAsync(string cardId, string pin);
}