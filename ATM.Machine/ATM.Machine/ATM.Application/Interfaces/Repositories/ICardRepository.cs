using System;
using ATM.Domain.Entities;

namespace ATM.Application.Interfaces.Repositories;

public interface ICardRepository
{
    Task<bool> ExistsAsync(string id);
    Task<bool> VerifyPinAsync(string cardId, string pin);
}