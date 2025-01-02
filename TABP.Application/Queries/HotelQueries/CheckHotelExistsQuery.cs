using System.Diagnostics;
using MediatR;

namespace Application.Queries.HotelQueries;

public record CheckIfHotelExistsQuery : IRequest<bool>
{
    public Guid Id { get; set; }
}