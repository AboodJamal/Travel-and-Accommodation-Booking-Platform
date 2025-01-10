using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TABP.Hashing.PasswordUtils;
using TABP.Infrastructure.Authentication.User;

namespace TABP.Infrastructure.Authentication.Generators;

public class TokenGenerator : TokenGeneratorInterface
{
    private readonly AuthUserInterface _authUser;
    private readonly PasswordHandlerInterface _passwordHandler;
    private readonly IConfiguration _configuration;

    public TokenGenerator(AuthUserInterface authUser, PasswordHandlerInterface passwordGenerator, IConfiguration configuration)
    {
        _authUser = authUser;
        _passwordHandler = passwordGenerator;
        _configuration = configuration;
    }

    public async Task<string> GenerateToken(string email, string password, string secretKey, string issuer, string audience)
    {
        var user = await VerifyUserCredentials(email, password);
        var securityKey =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>
        {
            new("Email:",user.Email),
            new("Role:", user.Role.ToString()),
            new("Name:", user.FirstName + " " + user.LastName)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer,
            audience,
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow
            .AddMinutes(int.Parse(
            _configuration["JWTAuthenticationSettings:TokenTTL"]
            ?? "90")),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public async Task<User.User?> VerifyUserCredentials(string email, string password)
    {
        var user = await _authUser.GetUserAsync(email);
        if (user is null) return null;
        bool isPasswordMatch = _passwordHandler
                              .VerifyUserPassword(
                              password,
                              user.Password,
                              Convert.FromBase64String(user.Salt));
        return isPasswordMatch ? user : null;
    }
}