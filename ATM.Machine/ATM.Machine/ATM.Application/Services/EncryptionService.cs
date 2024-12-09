using System;
using System.Security.Cryptography;
using System.Text;
using ATM.Application.Interfaces.Security;

namespace ATM.Application.Services;

public class EncryptionService : IEncryptionService
{
    public string Hash(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] hashBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hashBytes);
        }
    }

    public bool Verify(string input, string hash)
    {
        string inputHash = Hash(input);
        return hash == inputHash;
    }
}