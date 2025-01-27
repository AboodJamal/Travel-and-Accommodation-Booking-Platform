using TABP.Application.DTOs.UserDtos;
using FluentValidation;
using TABP.API.Extra;

namespace TAABP.API.Features.Auth.AuthValidators;

public class SignUpRequestBodyValidator : GenericValidator<UserCreationDto>
{
    public SignUpRequestBodyValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("email address is Invalid ");

        RuleFor(x => x.FirstName)
            .NotEmpty();
        
        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
             .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch)))
            .WithMessage("Password must contain at least one special character (e.g -> !@#$%^&*()_-=+")
            .Must(password => password.Any(char.IsLower) && password.Any(char.IsUpper))
            .WithMessage("Password must contain both uppercase and lowercase characters.")
            .Must(password => password.Any(char.IsDigit))
            .WithMessage("Password must contain at least one number");
    }
}