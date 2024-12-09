using System;

namespace ATM.Application.Interfaces.Security;

public interface IEncryptionService
{
    string Hash(string input);
    bool Verify(string input, string hash);
}
