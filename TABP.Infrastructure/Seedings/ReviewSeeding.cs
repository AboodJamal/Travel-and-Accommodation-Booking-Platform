using TABP.Domain.Entities;

namespace Infrastructure.Common.Persistence.Seeding
{
    public class ReviewSeeding
    {
        public static IEnumerable<Review> SeedData()
        {
            return new List<Review>
            {
                new()
                {
                    Id = new Guid("d6c8fe16-8f4b-47a7-99a0-c55693f253b9"),
                    BookingId = new Guid("d4b8e2b5-4a47-4f7d-9571-1018a3e8745f"), // Match BookingId from BookingSeeding
                    Comment = "Had a fantastic experience, would definitely stay again!",
                    ReviewDate = DateTime.Parse("2023-04-10"),
                    Rating = 4.9f
                },
                new()
                {
                    Id = new Guid("f1f4d8f0-e8bc-4ed3-bb99-435b8ff7d8ab"),
                    BookingId = new Guid("bbf9562b-3a0d-4729-a421-55e2a84f9a0d"), // Match BookingId from BookingSeeding
                    Comment = "Great value for the price, will recommend to friends.",
                    ReviewDate = DateTime.Parse("2023-05-15"),
                    Rating = 4.3f
                },
                new()
                {
                    Id = new Guid("e85f951d-b0f1-4655-90f1-8bcdb563b3c2"),
                    BookingId = new Guid("cd0a6077-c3a7-4d56-8356-12a6de4e7a82"), // Match BookingId from BookingSeeding
                    Comment = "Good location, but the room could be cleaner.",
                    ReviewDate = DateTime.Parse("2023-06-20"),
                    Rating = 3.8f
                }
            };
        }
    }
}
