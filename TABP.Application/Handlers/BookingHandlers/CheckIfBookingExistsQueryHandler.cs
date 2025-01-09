using Application.Queries.BookingQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.BookingHandlers;

public class CheckIfBookingExistsQueryHandler : IRequestHandler<CheckIfBookingExistsQuery, bool>
{
    private readonly BookingRepositoryInterface _bookingRepository;

    public CheckIfBookingExistsQueryHandler(BookingRepositoryInterface bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<bool> Handle(CheckIfBookingExistsQuery request, CancellationToken cancellationToken)
    {
        return await _bookingRepository.IsExistAsync(request.Id);
    }
}