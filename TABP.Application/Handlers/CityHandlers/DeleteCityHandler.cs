using Application.Commands.CityCommands;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Exceptions;
using MediatR;

namespace Application.Handlers.CityHandlers;

public class DeleteCityHandler : IRequestHandler<DeleteCityCommand>
{
    private readonly CityRepositoryInterface _cityRepository;
    private readonly IMapper _mapper;

    public DeleteCityHandler(CityRepositoryInterface cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        if (!await _cityRepository.IsExistAsync(request.Id))
        {
            throw new NotFoundException("City Doesn't Exists To Delete");
        }
        await _cityRepository.DeleteAsync(request.Id);
    }
}