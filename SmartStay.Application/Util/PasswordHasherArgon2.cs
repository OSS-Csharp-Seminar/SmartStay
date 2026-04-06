using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using SmartStay.Application.Interfaces;

namespace SmartStay.Application.Util;

public class PasswordHasherArgon2 : IPasswordHasher<string>//M.G: add exceptions later and multithread it!
{

    public string Hash(string password)
    {
        var config = new Argon2Config
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            MemoryCost = 65536,
            TimeCost = 3,
            Lanes = 4,
            Threads = 4,
            Password = Encoding.UTF8.GetBytes(password),
            Salt = RandomNumberGenerator.GetBytes(16),
            HashLength = 32
        };
        
        return Argon2.Hash(config);
    }

    public bool Validate(string password, string hashedPassword)
    {
        return Argon2.Verify(hashedPassword,password);
    }
}