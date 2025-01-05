using Application.Commands.BookingCommands;
using TABP.Application.DTOs.BookingDtos;
using Application.Queries.BookingQueries;
using AutoMapper;
using Infrastructure.ExtraModels;
using TABP.Domain.Entities;

namespace Application.Profiles;

public class BookingProfileMapper : Profile
{
    public BookingProfileMapper()
    {
        CreateMap<Booking, BookingDto>();
        CreateMap<Invoice, InvoiceDto>(); 
        CreateMap<BookingQueryDto, GetBookingsByHotelIdQuery>();
        CreateMap<ReserveRoomDto, BookRoomCommand>();
        CreateMap<BookRoomCommand, Booking>();
    }
}