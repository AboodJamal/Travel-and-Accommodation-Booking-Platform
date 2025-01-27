using TABP.Application.DTOs.BookingDtos;
using FluentValidation;
using TABP.API.Extra;

namespace TABP.API.Features.Booking.BookingValidators;

public class ReserveRoomValidator : GenericValidator<ReserveRoomDto>
{
    public ReserveRoomValidator()
    {
        RuleFor(booking => booking.CheckInDate)
            .GreaterThan(DateTime.Today)
            .WithMessage("Can't book a room in the past for check-in");
        
        RuleFor(booking => booking.CheckOutDate)
            .GreaterThanOrEqualTo(booking => booking.CheckInDate.AddDays(1))
            .WithMessage("Check-out date must be after the check-in date");
    }
}