using Application.Queries.BookingQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.BookingHandlers;

public class CheckIfBookingExistsForGuestQueryHandler : IRequestHandler<CheckIfBookingExistsForGuestQuery, bool>
{
    private readonly BookingRepositoryInterface _bookingRepository;

    public CheckIfBookingExistsForGuestQueryHandler(BookingRepositoryInterface bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<bool> Handle(CheckIfBookingExistsForGuestQuery request, CancellationToken cancellationToken)
    {
        return await _bookingRepository
               .BookingExistsForGuestAsync
               (request.BookingId, request.GuestEmail);
    }
}