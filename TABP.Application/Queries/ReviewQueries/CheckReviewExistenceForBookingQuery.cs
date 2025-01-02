using MediatR;

namespace Application.Queries.ReviewQueries;

public record CheckIfReviewExistsForBookingQuery : IRequest<bool>
{
    public Guid BookingId { get; set; }
}