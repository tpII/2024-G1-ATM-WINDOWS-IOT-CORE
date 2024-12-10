using System;
using ATM.Domain.Entities;

namespace ATM.Application.Interfaces.Repositories;

public interface ICardRepository
{
    Task<string?> ExistsAsync(string number);
    Task<string?> VerifyPinAsync(string cardId, string pin);
}