using Application.Commands.HotelCommands;
using TABP.Application.DTOs.HotelDtos;
using TABP.Application.DTOs.RoomDtos;
using Application.Queries.CityQueries;
using Application.Queries.HotelQueries;
using AutoMapper;
using Infrastructure.ExtraModels;
using TABP.Domain.Entities;

namespace Application.Profiles;

public class HotelProfileMapper : Profile
{
    public HotelProfileMapper()
    {
        CreateMap<Hotel, HotelDto>();
        CreateMap<HotelDto, HotelNoRoomsDto>();
        CreateMap<Hotel, HotelNoRoomsDto>();
        CreateMap<HotelCreationDto, CreateHotelCommand>();
        CreateMap<CreateHotelCommand, Hotel>();
        CreateMap<Hotel, HotelNoRoomsDto>();
        CreateMap<HotelUpdateDto, UpdateHotelCommand>();
        CreateMap<UpdateHotelCommand, Hotel>();
        CreateMap<GetHotelAvailableRoomsDto, GetHotelAvailableRoomsQuery>();
        CreateMap<HotelSearchQuery, ParametersOfHotelSearch>();
        CreateMap<FeaturedDeal, FeaturedDealDto>();
    }
}