using Application.Commands.HotelCommands;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.HotelHandlers;

public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand>
{
    private readonly HotelRepositoryInterface _hotelRepository;
    private readonly IMapper _mapper;

    public UpdateHotelCommandHandler(HotelRepositoryInterface hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        if (!await _hotelRepository.IsExistAsync(request.Id))
        {
            throw new NotFoundException($"Hotel with ID : {request.Id}");
        }
        var roomAmenityToUpdate = _mapper.Map<Hotel>(request);
        await _hotelRepository.UpdateAsync(roomAmenityToUpdate);
    }
}