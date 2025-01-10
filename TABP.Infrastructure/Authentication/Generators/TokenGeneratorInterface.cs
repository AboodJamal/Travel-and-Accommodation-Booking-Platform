using TABP.Infrastructure.Authentication.User;

namespace TABP.Infrastructure.Authentication.Generators;

public interface TokenGeneratorInterface
{
    public Task<string> GenerateToken(
        string email,
        string password,
        string secretKey,
        string issuer,
        string audience);

    public Task<User.User?> VerifyUserCredentials(string email, string password);
}