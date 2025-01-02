using TABP.Domain.Entities;

namespace Infrastructure.Common.Persistence.Seeding;

public class CitySeeding
{
    public static IEnumerable<City> SeedData()
    {
        return new List<City>
        {
            new()
            {
                Id = new Guid("a23d7e4f-0c9d-4d91-bb2e-df8f2fa12a6e"),
                Name = "Berlin",
                CountryName = "Germany",
                PostOffice = "BER",
                CountryCode = "DE"
            },
            new()
            {
                Id = new Guid("6d7b2be5-455f-4bbf-91ae-87b95b5fbb89"),
                Name = "Paris",
                CountryName = "France",
                PostOffice = "PAR",
                CountryCode = "FR"
            },
            new()
            {
                Id = new Guid("ec9d0150-b648-4d0d-9149-8be65f7d0b10"),
                Name = "Sydney",
                CountryName = "Australia",
                PostOffice = "SYD",
                CountryCode = "AU"
            }
        };
    }
}
