using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace TABP.Infrastructure.Authentication.User;

public class AuthUser : AuthUserInterface
{
    private readonly InfrastructureDbContext _context;

    public AuthUser(InfrastructureDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserAsync(string email)
    {
        var user = await _context
            .Users
            .SingleOrDefaultAsync(appUser => appUser.Email.Equals(email));

        if (user is null) return null;
        return new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.PasswordHash,
            Role = user.Role,
            Salt = user.Salt
        };
    }
}