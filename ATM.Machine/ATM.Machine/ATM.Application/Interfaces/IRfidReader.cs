using System;
using System.Threading;

namespace ATM.Application.Interfaces;

public interface IRfidReader : IDisposable
{
    Task<byte[]> ReadCardIdAsync(CancellationToken cancellationToken);
}
