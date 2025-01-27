using TABP.Application.DTOs.ReviewsDtos;
using FluentValidation;
using TABP.API.Extra;

namespace TAABP.API.Features.Review.ReviewValidators;

public class CreateReviewValidator : GenericValidator<ReviewCreationDto>
{
    public CreateReviewValidator()
    {
        RuleFor(review => review.BookingId)
            .NotEmpty().WithMessage("BookingId field shouldn't be empty");

        RuleFor(review => review.Comment)
            .NotEmpty()
            .WithMessage("Comment field shouldn't be empty");

        RuleFor(review => review.Rating)
            .InclusiveBetween(0, 5)
            .WithMessage("Rating must be between 0 and 5.");
    }
}