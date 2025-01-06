using Application.Commands.RoomAmenityCommands;
using TABP.Application.DTOs.RoomAmenityDtos;
using AutoMapper;
using TABP.Domain.Entities;

namespace TABP.Application.ApplicationServices.ProfileMappers;

public class RoomAmenityProfileMapper : Profile
{
    public RoomAmenityProfileMapper()
    {
        CreateMap<RoomAmenity, RoomAmenityDto>();
        CreateMap<RoomAmenityDto, RoomAmenityUpdateDto>();
        CreateMap<RoomAmenityCreationDto, CreateRoomAmenityCommand>();
        CreateMap<CreateRoomAmenityCommand, RoomAmenity>();
        CreateMap<RoomAmenityUpdateDto, UpdateRoomAmenityCommand>();
        CreateMap<UpdateRoomAmenityCommand, RoomAmenity>();
    }
}