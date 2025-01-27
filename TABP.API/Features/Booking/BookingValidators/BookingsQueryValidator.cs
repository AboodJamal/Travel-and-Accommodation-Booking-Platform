using Application.Queries.BookingQueries;
using FluentValidation;
using TABP.API.Extra;
namespace TABP.API.Features.Booking.BookingValidators;

public class BookingsQueryValidator : GenericValidator<GetBookingsByHotelIdQuery>
{
    public BookingsQueryValidator()
    {
        RuleFor(booking => booking.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(booking => booking.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.")
            .LessThan(11)
            .WithMessage("Page Size can't be greater than 10");
    }
}