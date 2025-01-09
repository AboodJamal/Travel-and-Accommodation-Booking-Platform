using Application.Queries.RoomQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.RoomHandlers;

public class CheckRoomExistsQueryHandler : IRequestHandler<CheckRoomExistsQuery, bool>
{
    private readonly RoomRepositoryInterface _roomRepository;

    public CheckRoomExistsQueryHandler(RoomRepositoryInterface roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<bool> Handle(CheckRoomExistsQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.IsExistAsync(request.Id);
    }
}