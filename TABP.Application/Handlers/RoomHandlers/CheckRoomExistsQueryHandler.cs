using Application.Queries.RoomQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.RoomHandlers;

public class CheckIfRoomExistsQueryHandler : IRequestHandler<CheckIfRoomExistsQuery, bool>
{
    private readonly RoomRepositoryInterface _roomRepository;

    public CheckIfRoomExistsQueryHandler(RoomRepositoryInterface roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<bool> Handle(CheckIfRoomExistsQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.IsExistAsync(request.Id);
    }
}