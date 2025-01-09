using Application.Queries.BookingQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.BookingHandlers;

public class BookingExistsForGuestQueryHandler : IRequestHandler<BookingExistsForGuestQuery, bool>
{
    private readonly BookingRepositoryInterface _bookingRepository;

    public BookingExistsForGuestQueryHandler(BookingRepositoryInterface bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<bool> Handle(BookingExistsForGuestQuery request, CancellationToken cancellationToken)
    {
        return await _bookingRepository
               .BookingExistsForGuestAsync
               (request.BookingId, request.GuestEmail);
    }
}