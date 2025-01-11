using Application.Commands.UserCommands;
using TABP.Application.DTOs.UserDtos;
using AutoMapper;
using Infrastructure.ExtraModels;
using TABP.Infrastructure.Authentication.Generators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TAABP.API.Validators.AuthValidators;
using Infrastructure.Authentication;


namespace TAABP.API.Controllers;

[ApiController]
[Route("api/authentication")]
[ApiVersion("1")]
public class AuthenticationController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly TokenGeneratorInterface _tokenGenerator;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthenticationController(IConfiguration configuration,
        TokenGeneratorInterface tokenGenerator,
        IMapper mapper,
        IMediator mediator)
    {
        _configuration = configuration;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    /// <summary>
    /// Endpoint for user sign-in. Validates user credentials and
    /// generates a JWT token upon successful authentication.
    /// </summary>
    /// <param name="email">The email address of the user attempting to sign in.</param>
    /// <param name="password">The password associated with the user's account.</param>
    /// <returns>
    /// If successful, returns the generated JWT token;
    /// otherwise, returns a list of validation errors or unauthorized status.
    /// </returns>
    [HttpPost("sign-In")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> SignIn(
        AuthenticationRequestBody authenticationRequestBody)
    {
        var validator = new SignInRequestBodyValidator();
        var errors = await validator.CheckForValidationErrorsAsync(authenticationRequestBody);
        if (errors.Count > 0) return BadRequest(errors);
        
        var user = await _tokenGenerator.VerifyUserCredentials(
            authenticationRequestBody.Email,
            authenticationRequestBody.Password);
        if (user is null) return Unauthorized();
        
        var secretKey = _configuration["JWTAuthenticationSettings:SecretForKey"];
        var issuer = _configuration["JWTAuthenticationSettings:Issuer"];
        var audience = _configuration["JWTAuthenticationSettings:Audience"];
        var token = await _tokenGenerator.GenerateToken(
                                   user.Email,
                                   authenticationRequestBody.Password,
                                   secretKey,
                                   issuer,
                                   audience);
        return Ok(token);
    }

    /// <summary>
    /// Registers a new user with the provided credentials.
    /// </summary>
    /// <param name="appUserForCreationDto">User registration details.</param>
    /// <returns>An action result indicating success or failure of the registration process.</returns>
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> SignUp(UserCreationDto appUserForCreationDto)
    {
        try
        {
            var validator = new SignUpRequestBodyValidator();
            var errors = await validator.CheckForValidationErrorsAsync(appUserForCreationDto);
            if (errors.Count > 0) return BadRequest(errors);
            
            var request = _mapper.Map<CreateUserCommand>(appUserForCreationDto);
            await _mediator.Send(request);
            return Ok("Register User Successfully.");
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }
}
