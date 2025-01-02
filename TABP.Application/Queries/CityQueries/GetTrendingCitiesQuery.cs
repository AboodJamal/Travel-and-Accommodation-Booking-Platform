using TABP.Application.DTOs.CityDtos;
using MediatR;

namespace Application.Queries.CityQueries;

public record GetTrendingCitiesQuery : IRequest<List<CityNoHotelsDto>>
{
    public int Count { get; set; } = 5;
}