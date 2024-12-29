using Application.DTOs.HotelDtos;

namespace TABP.Application.DTOs.CityDtos;

public record CityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
    public IList<HotelWithoutRoomsDto> Hotels { get; set; }
}