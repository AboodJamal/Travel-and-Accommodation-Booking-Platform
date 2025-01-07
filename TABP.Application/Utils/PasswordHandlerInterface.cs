namespace TABP.Application.Utils;

public interface PasswordHandlerInterface
{
    public string? GenerateHashedPassword(string password, byte[] salt);
    public byte[] GenerateHashingSaltValue(); // arr of bytes
}