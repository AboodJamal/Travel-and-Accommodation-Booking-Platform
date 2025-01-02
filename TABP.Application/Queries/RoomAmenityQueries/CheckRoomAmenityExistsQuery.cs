using MediatR;

namespace Application.Queries.RoomAmenityQueries;

public record CheckIfRoomAmenityExistsQuery : IRequest<bool>
{
    public Guid Id { get; set; }
}