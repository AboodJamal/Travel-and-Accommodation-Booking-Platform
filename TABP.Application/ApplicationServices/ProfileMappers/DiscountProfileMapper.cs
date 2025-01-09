using Application.Commands.DiscountCommands;
using TABP.Application.DTOs.DiscountDtos;
using Application.Queries.DiscountQueries;
using AutoMapper;
using TABP.Domain.Entities;

namespace TABP.Application.ApplicationServices.ProfileMappers;

public class DiscountProfileMapper : Profile
{
    public DiscountProfileMapper()
    {
        CreateMap<Discount, DiscountDto>();
        CreateMap<DiscountDto, Discount>();
        CreateMap<GetAllRoomTypeDiscountsDto, GetAllRoomTypeDiscountsQuery>();
        CreateMap<DiscountCreationDto, CreateDiscountCommand>();
        CreateMap<CreateDiscountCommand, Discount>();
    }
}