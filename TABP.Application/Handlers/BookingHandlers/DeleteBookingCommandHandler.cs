using Application.Commands.BookingCommands;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.BookingHandlers;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly BookingRepositoryInterface _bookingRepository;

    public DeleteBookingCommandHandler(BookingRepositoryInterface bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        await _bookingRepository.DeleteAsync(request.Id);
    }
}