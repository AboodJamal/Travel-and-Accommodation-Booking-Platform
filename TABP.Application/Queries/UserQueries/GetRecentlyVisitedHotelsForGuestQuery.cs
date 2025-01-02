using TABP.Application.DTOs.HotelDtos;
using MediatR;

namespace Application.Queries.UserQueries;

public record GetRecentlyVisitedHotelsForGuestQuery : IRequest<List<HotelNoRoomsDto>>
{
    public Guid GuestId { get; set; }
    public int Count { get; set; } = 5;
}