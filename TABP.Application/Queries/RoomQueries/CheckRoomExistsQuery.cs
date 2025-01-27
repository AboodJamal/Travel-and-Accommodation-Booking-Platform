using MediatR;

namespace Application.Queries.RoomQueries;

public record CheckIfRoomExistsQuery : IRequest<bool>
{
    public Guid Id { get; set; }
}