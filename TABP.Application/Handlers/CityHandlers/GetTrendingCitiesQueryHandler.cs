using TABP.Application.DTOs.CityDtos;
using Application.Queries.CityQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.CityHandlers;

public class GetTrendingCitiesQueryHandler : IRequestHandler<GetTrendingCitiesQuery,List<CityNoHotelsDto>>
{
    private readonly CityRepositoryInterface _cityRepository;
    private readonly IMapper _mapper;

    public GetTrendingCitiesQueryHandler(CityRepositoryInterface cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<List<CityNoHotelsDto>> Handle(GetTrendingCitiesQuery request, CancellationToken cancellationToken)
    {
        var trendingCities = await _cityRepository.GetTrendingCitiesAsync(request.Count);
        return _mapper.Map<List<CityNoHotelsDto>>(trendingCities);
    }
}