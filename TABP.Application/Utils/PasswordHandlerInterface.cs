namespace TABP.Application.Utils;

public interface PasswordHandlerInterface
{
    public string? GenerateHashedPassword(string password, byte[] saltValue);
    public byte[] GenerateHashingSaltValue(); // arr of bytes
    public bool VerifyUserPassword(string password, string hashedPassword, byte[] saltValue);
}