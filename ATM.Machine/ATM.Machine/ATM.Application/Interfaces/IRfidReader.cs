using System;

namespace ATM.Application.Interfaces;

public interface IRfidReader
{
    Task<string> ReadCardIdAsync();
}
