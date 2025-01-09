using Application.Commands.CityCommands;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using Infrastructure.Exceptions;
using MediatR;

namespace Application.Handlers.CityHandlers;

public class UpdateCityHandler : IRequestHandler<UpdateCityCommand>
{
    private readonly CityRepositoryInterface _cityRepository;
    private readonly IMapper _mapper;

    public UpdateCityHandler(CityRepositoryInterface cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        if (!await _cityRepository.IsExistAsync(request.Id))
        {
            throw new NotFoundException($"City With ID: {request.Id} ");
        }

        var cityToUpdate = _mapper.Map<City>(request);
        await _cityRepository.UpdateAsync(cityToUpdate);
    }
}