using Application.Commands.CityCommands;
using TABP.Application.DTOs.CityDtos;
using AutoMapper;
using TABP.Domain.Entities;

namespace Application.Profiles;

public class CityProfileMapper : Profile
{
    public CityProfileMapper()
    {
        CreateMap<City, CityDto>().ForMember (cityDto => 
        cityDto.Hotels,opt => opt.MapFrom(city => city.Hotels));
        CreateMap<CityDto, CityNoHotelsDto>();
        CreateMap<City, CityNoHotelsDto>();
        CreateMap<CityDto, CityUpdateDto>();
        CreateMap<CityCreationDto, CreateCityCommand>();
        CreateMap<CityUpdateDto, UpdateCityCommand>();
        CreateMap<UpdateCityCommand, City>();
        CreateMap<CreateCityCommand, City>();
    }
}