using TABP.Application.DTOs.DiscountDtos;
using Application.Queries.DiscountQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using Infrastructure.Exceptions;
using MediatR;

namespace Application.Handlers.DiscountHandlers;

public class GetAllRoomTypeDiscountsQueryHandler : IRequestHandler<GetAllRoomTypeDiscountsQuery, PaginatedList<DiscountDto>>
{
    private readonly DiscountRepositoryInterface _discountRepository;
    private readonly IMapper _mapper;

    public GetAllRoomTypeDiscountsQueryHandler(DiscountRepositoryInterface discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DiscountDto>> Handle(GetAllRoomTypeDiscountsQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await 
            _discountRepository
                .GetAllByRoomTypeIdAsync(
                    request.RoomTypeId,
                    request.PageNumber,
                    request.PageSize);

        return new PaginatedList<DiscountDto>(
            _mapper.Map<List<DiscountDto>>(paginatedList.Items),
            paginatedList.PageData);
    }
}