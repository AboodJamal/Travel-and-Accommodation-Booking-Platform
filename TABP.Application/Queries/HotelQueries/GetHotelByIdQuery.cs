using TABP.Application.DTOs.HotelDtos;
using MediatR;

namespace Application.Queries.HotelQueries;

public record GetHotelByIdQuery : IRequest<HotelNoRoomsDto?>
{
    public Guid Id { get; set; }
}