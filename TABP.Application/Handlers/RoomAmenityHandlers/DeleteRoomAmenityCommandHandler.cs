using Application.Commands.RoomAmenityCommands;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Exceptions;
using MediatR;

namespace Application.Handlers.RoomAmenityHandlers;

public class DeleteRoomAmenityCommandHandler : IRequestHandler<DeleteRoomAmenityCommand>
{
    private readonly RoomAmenityRepositoryInterface _roomAmenityRepository;
    private readonly IMapper _mapper;

    public DeleteRoomAmenityCommandHandler(RoomAmenityRepositoryInterface roomAmenityRepository)
    {
        _roomAmenityRepository = roomAmenityRepository;
    }
    
    public async Task Handle(DeleteRoomAmenityCommand request, CancellationToken cancellationToken)
    {
        if (!await _roomAmenityRepository.IsExistAsync(request.Id))
        {
            throw new NotFoundException("Room Amenity Doesn't Exists To Delete");
        }
        await _roomAmenityRepository.DeleteAsync(request.Id);
    }
}