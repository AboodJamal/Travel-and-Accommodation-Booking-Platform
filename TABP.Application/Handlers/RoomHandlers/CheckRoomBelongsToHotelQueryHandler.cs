using Application.Queries.RoomQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.RoomHandlers;

public record CheckRoomBelongsToHotelQueryHandler : IRequestHandler<CheckRoomBelongsToHotelQuery, bool>
{
    private readonly RoomRepositoryInterface _roomRepository;

    public CheckRoomBelongsToHotelQueryHandler(RoomRepositoryInterface roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<bool> Handle(CheckRoomBelongsToHotelQuery request,
    CancellationToken cancellationToken)
    {
        return await _roomRepository.
        IsRoomInHotelAsync(request.HotelId, request.RoomId);
    }
}