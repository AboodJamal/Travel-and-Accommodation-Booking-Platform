using Application.Commands.RoomCommands;
using TABP.Application.DTOs.RoomDtos;
using Application.Queries.RoomQueries;
using AutoMapper;
using TABP.Domain.Entities;

namespace TABP.Application.ApplicationServices.ProfileMappers;

public class RoomProfileMapper : Profile
{
    public RoomProfileMapper()
    {
        CreateMap<Room, RoomDto>();
        CreateMap<CreateRoomCommand, Room>();
        CreateMap<GetRoomsByHotelIdDto, GetRoomsByHotelIdQuery>();
        CreateMap<RoomCreationDto, CreateRoomCommand>();
    }
}