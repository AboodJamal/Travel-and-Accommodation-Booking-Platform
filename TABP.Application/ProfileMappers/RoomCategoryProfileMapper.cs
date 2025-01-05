using TABP.Application.DTOs.RoomCategoriesDtos;
using Application.Queries.RoomCategoryQueries;
using AutoMapper;
using TABP.Domain.Entities;

namespace Application.Profiles;

public class RoomCategoryProfileMapper : Profile
{
    public RoomCategoryProfileMapper()
    {
        CreateMap<RoomType, RoomTypeDto>().ForMember(roomDto => roomDto.Amenities,
        opt => opt.MapFrom(room => room.Amenities));
        CreateMap<RoomTypeDto, RoomCategoryWithoutAmenitiesDto>();
        CreateMap<GetRoomCategoriesByHotelIdDto, GetRoomCategoriesByHotelIdQuery>();
    }
}