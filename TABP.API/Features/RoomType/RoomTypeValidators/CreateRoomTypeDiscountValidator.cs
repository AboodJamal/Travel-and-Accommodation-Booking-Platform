﻿using Application.Commands.DiscountCommands;
using FluentValidation;
using TABP.API.Extra;


namespace TABP.API.Features.RoomType.RoomTypeValidators;

public class CreateRoomTypeDiscountValidator : GenericValidator<CreateDiscountCommand>
{
    public CreateRoomTypeDiscountValidator()
    {
        RuleFor(discount => discount.RoomTypeId)
            .NotEmpty().WithMessage("RoomTypeId field shouldn't be empty");

        RuleFor(discount => discount.DiscountPercentage)
            .InclusiveBetween(0.0f, 1.0f)
            .WithMessage("Discount must be between 0.0 and 1.0");

        RuleFor(discount => discount.FromDate)
            .NotEmpty()
            .WithMessage("FromDate field shouldn't be empty");

        RuleFor(discount => discount.ToDate)
            .NotEmpty()
            .WithMessage("ToDate field shouldn't be empty")
            .GreaterThanOrEqualTo(booking => booking.FromDate)
            .WithMessage("ToDate must be greater than or equal to FromDate");
    }
}
