using TABP.Domain.Entities;

public static class BookingSeeding
{
    public static IEnumerable<Booking> SeedData()
    {
        return new List<Booking>
        {
            new Booking
            {
                Id = new Guid("d4b8e2b5-4a47-4f7d-9571-1018a3e8745f"),
                RoomId = new Guid("b362b1ae-4f39-453f-b0f3-5a8f9d1b2815"),
                UserId = new Guid("473c85c7-8e77-4bc3-b5fa-1d27c5f9d2f1"),  // Foreign key to User
                Price = 120,
                CheckInDate = DateTime.Parse("2024-01-10"),
                CheckOutDate = DateTime.Parse("2024-01-15"),
                BookingDate = DateTime.Parse("2024-01-05")
            },
            new Booking
            {
                Id = new Guid("bbf9562b-3a0d-4729-a421-55e2a84f9a0d"),
                RoomId = new Guid("aa08b4e7-cbbc-4661-9bc3-2b2333bfe4de"),
                UserId = new Guid("9dcbf1b8-1a88-47b1-bc74-76c2fd10f23a"),  // Foreign key to User
                Price = 175,
                CheckInDate = DateTime.Parse("2024-02-18"),
                CheckOutDate = DateTime.Parse("2024-02-22"),
                BookingDate = DateTime.Parse("2024-02-10")
            },
            new Booking
            {
                Id = new Guid("cd0a6077-c3a7-4d56-8356-12a6de4e7a82"),
                RoomId = new Guid("8ac05d5d-f8d9-49de-bf2d-2746763b1459"),
                UserId = new Guid("a9d0a22f-5411-4c76-b1cc-056b9b400a61"),  // Foreign key to User
                Price = 250,
                CheckInDate = DateTime.Parse("2024-03-01"),
                CheckOutDate = DateTime.Parse("2024-03-05"),
                BookingDate = DateTime.Parse("2024-02-25")
            }
        };
    }
}
