namespace TABP.Application.DTOs.CityDtos;

public record CityNoHotelsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
}