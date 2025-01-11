using TABP.Application.DTOs.HotelDtos;

namespace TABP.Application.DTOs.CityDtos;

public record CityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
    public IList<HotelNoRoomsDto> Hotels { get; set; }
}