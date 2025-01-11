using TABP.Application.DTOs.CityDtos;
using FluentValidation;
using TABP.API.Extra;

namespace TABP.API.Features.City.CityValidators;

public class CreateCityValidator : GenericValidator<CityCreationDto>
{
    public CreateCityValidator()
    {
        RuleFor(city => city.Name)
            .NotEmpty()
            .WithMessage("Name field can't be empty or null");

        RuleFor(city => city.CountryCode)
            .NotEmpty()
            .Must(city => city.Length >= 2)
            .WithMessage("CountryCode must be greater than or equal to 2 characters");
        
        RuleFor(city => city.PostOffice)
            .NotEmpty()
            .WithMessage("PostOffice field can't be empty or null");
        
        RuleFor(city => city.CountryName)
            .NotEmpty()
            .WithMessage("Country field can't be empty or null");
    }
}