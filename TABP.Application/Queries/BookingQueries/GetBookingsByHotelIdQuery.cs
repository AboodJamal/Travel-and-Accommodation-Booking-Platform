using TABP.Application.DTOs.BookingDtos;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Queries.BookingQueries;

public record GetBookingsByHotelIdQuery : IRequest<PaginatedList<BookingDto>>
{
    public Guid HotelId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}