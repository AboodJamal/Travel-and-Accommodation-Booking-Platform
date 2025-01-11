using TABP.Application.DTOs.CityDtos;
using FluentValidation;
using TABP.API.Extra;

namespace TABP.API.Features.City.CityValidators;

public class UpdateCityValidator : GenericValidator<CityUpdateDto>
{
    public UpdateCityValidator()
    {
        RuleFor(city => city.Name)
            .NotEmpty()
            .WithMessage("Name field can't be empty or null");

        RuleFor(city => city.CountryCode)
            .NotEmpty()
            .Must(city => city.Length == 3)
            .WithMessage("CountryCode must be exactly 3 characters");
        
        RuleFor(city => city.PostOffice)
            .NotEmpty()
            .WithMessage("PostOffice field can't be empty or null");
        
        RuleFor(city => city.CountryName)
            .NotEmpty()
            .WithMessage("Country field can't be empty or null");
    }
}