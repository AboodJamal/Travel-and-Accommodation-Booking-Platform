using TABP.Domain.Entities;

namespace Infrastructure.Common.Persistence.Seeding;

public class HotelSeeding
{
    public static IEnumerable<Hotel> SeedData()
    {
        return new List<Hotel>
        {
            new()
            {
                Id = new Guid("a3d5c7b1-36fa-4b52-bc42-d8d7589478fd"),
                CityId = new Guid("a23d7e4f-0c9d-4d91-bb2e-df8f2fa12a6e"), // Correct CityId for Berlin
                OwnerId = new Guid("9bfc8bc4-8278-453b-9ff4-d075f960ea44"),
                Name = "Grand Plaza Hotel",
                Rating = 4.7f,
                StreetAddress = "1500 Skyline Blvd",
                Description = "An upscale hotel with panoramic city views.",
                PhoneNumber = "9876543210",
                FloorsNumber = 15
            },
            new()
            {
                Id = new Guid("bb507c23-35fd-4b6c-83c0-b3fc54d40d4b"),
                CityId = new Guid("6d7b2be5-455f-4bbf-91ae-87b95b5fbb89"), // Correct CityId for Paris
                OwnerId = new Guid("3a89e98d-cb27-4632-a7f9-36b163479e70"),
                Name = "Mountain Retreat",
                Rating = 4.1f,
                StreetAddress = "102 Pine Ridge Lane",
                Description = "A peaceful retreat in the heart of the mountains.",
                PhoneNumber = "6145678901",
                FloorsNumber = 6
            },
            new()
            {
                Id = new Guid("39a6cb7d-31c4-4752-bb6e-8c76a04e3e9f"),
                CityId = new Guid("ec9d0150-b648-4d0d-9149-8be65f7d0b10"), // Correct CityId for Sydney
                OwnerId = new Guid("b5e9f7b9-1a25-4c5c-b378-078c76df1f7a"),
                Name = "Seaside View Hotel",
                Rating = 4.3f,
                StreetAddress = "45 Oceanfront Way",
                Description = "A hotel offering stunning views of the ocean waves.",
                PhoneNumber = "7145678902",
                FloorsNumber = 8
            }
        };
    }
}
