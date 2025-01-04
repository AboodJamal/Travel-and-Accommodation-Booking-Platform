using Application.Queries.RoomAmenityQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.RoomAmenityHandlers;

public class CheckIfRoomAmenityExistsQueryHandler : IRequestHandler<CheckIfRoomAmenityExistsQuery, bool>
{
    private readonly RoomAmenityRepositoryInterface _roomAmenityRepository;

    public CheckIfRoomAmenityExistsQueryHandler(RoomAmenityRepositoryInterface amenityRepository)
    {
        _roomAmenityRepository = amenityRepository;
    }

    public async Task<bool> Handle(CheckIfRoomAmenityExistsQuery request, CancellationToken cancellationToken)
    {
        return await _roomAmenityRepository.IsExistAsync(request.Id);
    }
}