using Application.Commands.BookingCommands;
using TABP.Application.DTOs.BookingDtos;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using MediatR;

namespace Application.Handlers.BookingHandlers;

public class BookRoomCommandHandler : IRequestHandler<BookRoomCommand, BookingDto?>
{
    private readonly BookingRepositoryInterface _bookingRepository;   
    private readonly UserRepositoryInterface _userRepository;   
    private readonly RoomRepositoryInterface _roomRepository;   
    private readonly IMapper _mapper;                                                   

    public BookRoomCommandHandler(BookingRepositoryInterface bookingRepository, IMapper mapper, UserRepositoryInterface userRepository, RoomRepositoryInterface roomRepository)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _roomRepository = roomRepository;
    }

    public async Task<BookingDto?> Handle(BookRoomCommand request, CancellationToken cancellationToken)
    {
        var bookingToAdd = _mapper.Map<Booking>(request);

        var guestId = await _userRepository.GetGuestIdByEmailAsync(request.GuestEmail);
        if (guestId == null)
        {
            throw new InvalidOperationException($"Guest email '{request.GuestEmail}' not found.");
        }
        bookingToAdd.UserId = guestId.Value;

        bookingToAdd.Price = await _roomRepository.GetPriceForRoomWithDiscount(request.RoomId);

        var addedBooking = await _bookingRepository.InsertAsync(bookingToAdd);
        return _mapper.Map<BookingDto>(addedBooking);
    }

}