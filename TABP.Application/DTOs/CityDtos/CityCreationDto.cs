﻿namespace TABP.Application.DTOs.CityDtos;

public record CityCreationDto
{
    public string Name { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
}