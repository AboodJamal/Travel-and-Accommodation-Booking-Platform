namespace TABP.Infrastructure.Authentication.User;

public interface AuthUserInterface
{
    public Task<User?> GetUserAsync(string email);
}