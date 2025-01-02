using Domain.Enums;
using TABP.Domain.Entities;

public static class UserSeeding
{
    public static IEnumerable<User> SeedData()
    {
        return new List<User>
        {
            new User
            {
                Id = new Guid("473c85c7-8e77-4bc3-b5fa-1d27c5f9d2f1"),
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                PasswordHash = "hashedPassword1", // Just an example, replace with actual hash
                Salt = "salt1",
                Role = UserRole.Admin // Assuming you have an enum for UserRole
            },
            new User
            {
                Id = new Guid("9dcbf1b8-1a88-47b1-bc74-76c2fd10f23a"),
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alicesmith@example.com",
                PhoneNumber = "9876543210",
                PasswordHash = "hashedPassword2", // Just an example
                Salt = "salt2",
                Role = UserRole.Guest // Assuming you have an enum for UserRole
            },
            new User
            {
                Id = new Guid("a9d0a22f-5411-4c76-b1cc-056b9b400a61"),
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bobjohnson@example.com",
                PhoneNumber = "1122334455",
                PasswordHash = "hashedPassword3", // Just an example
                Salt = "salt3",
                Role = UserRole.Guest // Assuming you have an enum for UserRole
            }
        };
    }
}
