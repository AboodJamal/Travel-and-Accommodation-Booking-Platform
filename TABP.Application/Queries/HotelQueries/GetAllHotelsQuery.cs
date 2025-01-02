using TABP.Application.DTOs.HotelDtos;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Queries.HotelQueries;

public record GetAllHotelsQuery : IRequest<PaginatedList<HotelNoRoomsDto>>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}