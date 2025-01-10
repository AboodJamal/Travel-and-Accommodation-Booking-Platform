using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;

namespace TABP.Application.Utils;

public class PasswordHandler : PasswordHandlerInterface
{
    private readonly int _saltSize;
    private readonly int _timeCost;
    private readonly string _secret;
    private readonly int _hashLength;
    private readonly IConfiguration _configuration;

    public PasswordHandler(IConfiguration configuration)
    {
        _configuration = configuration;
        _saltSize = int.Parse(_configuration["PasswordHandlerSaltSize"]);
        _timeCost = int.Parse(_configuration["PasswordHandlerTimeCost"]);
        _secret = _configuration["PasswordHandlerSecret"];
        _hashLength = int.Parse(_configuration["PasswordHandlerHashLength"]);
    }

    public byte[] GenerateHashingSaltValue()
    {
        var salt = new byte[_saltSize];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    public string? GenerateHashedPassword(string password, byte[] saltValue)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty.");
        }
        try
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = _timeCost, 
                Threads = Environment.ProcessorCount,
                Password = passwordBytes,
                Salt = saltValue,
                Secret = Encoding.UTF8.GetBytes(_secret), 
                HashLength = _hashLength 
            };

            var argon2 = new Argon2(config);
            using var hashA = argon2.Hash();
            var hashString = config.EncodeString(hashA.Buffer);

            return hashString;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public bool VerifyUserPassword(string password, string hashedPassword, byte[] saltValue)
    {
        try
        {
            return hashedPassword.Equals(GenerateHashedPassword(password, saltValue));
        }
        catch (Exception)
        {
            return false;
        }
    }
}
