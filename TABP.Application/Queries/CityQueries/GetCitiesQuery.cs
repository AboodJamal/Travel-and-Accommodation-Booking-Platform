using TABP.Application.DTOs.CityDtos;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Queries.CityQueries;

public record GetCitiesQuery : IRequest<PaginatedList<CityDto>>
{
    public bool IncludeHotels { get; set; } = false;
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}