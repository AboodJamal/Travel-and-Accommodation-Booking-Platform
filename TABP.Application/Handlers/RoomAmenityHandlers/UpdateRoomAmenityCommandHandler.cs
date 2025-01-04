using Application.Commands.RoomAmenityCommands;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using Infrastructure.Exceptions;
using MediatR;

namespace Application.Handlers.RoomAmenityHandlers;

public class UpdateRoomAmenityCommandHandler : IRequestHandler<UpdateRoomAmenityCommand>
{
    private readonly RoomAmenityRepositoryInterface _roomAmenityRepository;
    private readonly IMapper _mapper;

    public UpdateRoomAmenityCommandHandler(RoomAmenityRepositoryInterface roomAmenityRepository, IMapper mapper)
    {
        _roomAmenityRepository = roomAmenityRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateRoomAmenityCommand request, CancellationToken cancellationToken)
    {
        if (!await _roomAmenityRepository.IsExistAsync(request.Id))
        {
            throw new NotFoundException($"Room Amenity with ID : {request.Id}");
        }
        var roomAmenityToUpdate = _mapper.Map<RoomAmenity>(request);
        await _roomAmenityRepository.UpdateAsync(roomAmenityToUpdate);
    }
}