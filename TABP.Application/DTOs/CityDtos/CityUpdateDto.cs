namespace TABP.Application.DTOs.CityDtos;

public record CityUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
}