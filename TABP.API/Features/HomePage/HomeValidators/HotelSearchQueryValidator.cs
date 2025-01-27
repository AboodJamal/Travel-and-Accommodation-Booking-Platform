using Application.Queries.HotelQueries;
using FluentValidation;
using TABP.API.Extra;

namespace TAABP.API.Features.HomePage.HomeValidators;

public class HotelSearchQueryValidator : GenericValidator<HotelSearchQuery>
{
    public HotelSearchQueryValidator()
    {
        RuleFor(query => query.CheckInDate)
            .GreaterThan(DateTime.Today)
            .WithMessage("Check-in must can't be in the past");

        RuleFor(query => query.CheckOutDate)
            .GreaterThanOrEqualTo(booking => booking.CheckInDate.AddDays(1))
        .WithMessage("Check-out date must be after check-in date");
        
        RuleFor(query => query.StarRate)
            .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5");
        
        RuleFor(query => query.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0");

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0");
        
        RuleFor(query => query.Adults)
            .GreaterThan(0)
            .WithMessage("Adults must be greater than 0");
        
        RuleFor(query => query.Children)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Children number must be greater than or equal 0");
    }
}