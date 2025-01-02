using TABP.Application.DTOs.HotelDtos;
using MediatR;

namespace Application.Queries.UserQueries;

public record GetRecentlyVisitedHotelsForAuthenticatedGuestQuery : IRequest<List<HotelNoRoomsDto>>
{
    public string Email { get; set; }
    public int Count { get; set; } = 5;
}