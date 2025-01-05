using Application.Commands.UserCommands;
using TABP.Application.DTOs.UserDtos;
using AutoMapper;
using TABP.Domain.Entities;

namespace Application.Profiles;

public class UserProfileMapper : Profile
{
    public UserProfileMapper()
    {
        CreateMap<CreateUserCommand, User>().ForMember(dest => dest.PasswordHash,
        opt => opt.MapFrom(src => src.Password));
        CreateMap<UserCreationDto, CreateUserCommand>();
    }
}