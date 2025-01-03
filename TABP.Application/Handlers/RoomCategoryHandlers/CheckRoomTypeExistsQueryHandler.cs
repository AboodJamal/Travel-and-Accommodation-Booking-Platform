using Application.Queries.RoomCategoryQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.RoomCategoryHandlers;

public class CheckRoomTypeExistsQueryHandler : IRequestHandler<CheckRoomTypeExistsQuery, bool>
{
    private readonly RoomTypeRepositoryInterface _roomTypeRepository;

    public CheckRoomTypeExistsQueryHandler(RoomTypeRepositoryInterface roomTypeRepository)
    {
        _roomTypeRepository = roomTypeRepository;
    }

    public Task<bool> Handle(CheckRoomTypeExistsQuery request, CancellationToken cancellationToken)
    {
        return _roomTypeRepository.IsExistAsync(request.Id);
    }
}