﻿using Application.Queries.HotelQueries;
using FluentValidation;
using TABP.API.Extra;

namespace TAABP.API.Validators.HotelValidators;

public class GetAllHotelsValidator : GenericValidator<GetAllHotelsQuery>
{
    public GetAllHotelsValidator()
    {
        RuleFor(roomAmenity => roomAmenity.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(roomAmenity => roomAmenity.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.");
    }
}