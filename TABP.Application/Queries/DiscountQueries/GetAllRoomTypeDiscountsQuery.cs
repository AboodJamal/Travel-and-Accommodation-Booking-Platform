using TABP.Application.DTOs.DiscountDtos;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Queries.DiscountQueries;

public record GetAllRoomTypeDiscountsQuery : IRequest<PaginatedList<DiscountDto>>
{
    public Guid RoomTypeId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}