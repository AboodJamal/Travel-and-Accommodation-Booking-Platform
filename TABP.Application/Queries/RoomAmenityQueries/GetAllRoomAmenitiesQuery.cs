using TABP.Application.DTOs.RoomAmenityDtos;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Queries.RoomAmenityQueries;

public record GetAllRoomAmenitiesQuery : IRequest<PaginatedList<RoomAmenityDto>>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}