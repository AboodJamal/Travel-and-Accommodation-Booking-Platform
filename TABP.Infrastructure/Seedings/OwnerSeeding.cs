using TABP.Domain.Entities;

namespace Infrastructure.Common.Persistence.Seeding
{
    public class OwnerSeeding
    {
        public static List<Owner> SeedData()
        {
            return new List<Owner>
            {
                new()
                {
                    Id = new Guid("9bfc8bc4-8278-453b-9ff4-d075f960ea44"),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    PhoneNumber = "1234567890",
                },
                new()
                {
                    Id = new Guid("3a89e98d-cb27-4632-a7f9-36b163479e70"),
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alicesmith@example.com",
                    PhoneNumber = "9876543210",
                },
                new()
                {
                    Id = new Guid("b5e9f7b9-1a25-4c5c-b378-078c76df1f7a"),
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "bobjohnson@example.com",
                    PhoneNumber = "1122334455",
                }
            };
        }
    }
}
