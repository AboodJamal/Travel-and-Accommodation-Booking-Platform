using Application.Commands.UserCommands;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using Domain.Enums;
using MediatR;
using TABP.Hashing.PasswordUtils;
namespace Application.Handlers.UserHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly UserRepositoryInterface _userRepository;
    private readonly IMapper _mapper;
    private readonly PasswordHandlerInterface _passwordGenerator;
    public CreateUserCommandHandler(UserRepositoryInterface userRepository, IMapper mapper, PasswordHandlerInterface passwordGenerator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordGenerator = passwordGenerator;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        var uniqueUserSalt = _passwordGenerator.GenerateHashingSaltValue();
        var userPasswordHash = _passwordGenerator.GenerateHashedPassword(request.Password, uniqueUserSalt);
        if (userPasswordHash is null) throw new InvalidOperationException("Can't Hash User Password");
        
        user.Id = Guid.NewGuid();
        user.PasswordHash = userPasswordHash;
        user.Salt = Convert.ToBase64String(uniqueUserSalt);
        user.Role = UserRole.Guest;
        await _userRepository.InsertAsync(user);
    }
}