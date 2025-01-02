using MediatR;

namespace Application.Queries.BookingQueries;

public record CheckIfBookingExistsQuery : IRequest<bool>
{
    public Guid Id { get; set; }
};