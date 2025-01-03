using TABP.Application.DTOs.CityDtos;
using Application.Queries.CityQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.CityHandlers;

public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityDto?>
{
    private readonly CityRepositoryInterface _cityRepository;
    private readonly IMapper _mapper;

    public GetCityByIdQueryHandler(CityRepositoryInterface cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<CityDto?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.Id, request.IncludeHotels);
        return _mapper.Map<CityDto>(city);
    }
}