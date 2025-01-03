using Application.Commands.CityCommands;
using TABP.Application.DTOs.CityDtos;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using MediatR;

namespace Application.Handlers.CityHandlers;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityNoHotelsDto?>
{
    private readonly CityRepositoryInterface _cityRepository;
    private readonly IMapper _mapper;

    public CreateCityCommandHandler(CityRepositoryInterface cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<CityNoHotelsDto?> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var cityToAdd = _mapper.Map<City>(request);
        var addedCity = await _cityRepository.InsertAsync(cityToAdd);
        return addedCity is null ? null : _mapper.Map<CityNoHotelsDto>(addedCity);
    }
}