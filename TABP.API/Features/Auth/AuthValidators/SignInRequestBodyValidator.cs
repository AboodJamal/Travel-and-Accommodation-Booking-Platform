using FluentValidation;
using Infrastructure.Authentication;
using TABP.API.Extra;

namespace TAABP.API.Features.Auth.AuthValidators;

public class SignInRequestBodyValidator : GenericValidator<AuthenticationRequestBody>
{
    public SignInRequestBodyValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch)))
            .WithMessage("Password must contain at least one special character (e.g -> !@#$%^&*()_-=+")
            .Must(password => password.Any(char.IsLower) && password.Any(char.IsUpper))
            .WithMessage("Password must contain both uppercase and lowercase characters.")
            .Must(password => password.Any(char.IsDigit))
            .WithMessage("Password must contain at least one number");
    }
}