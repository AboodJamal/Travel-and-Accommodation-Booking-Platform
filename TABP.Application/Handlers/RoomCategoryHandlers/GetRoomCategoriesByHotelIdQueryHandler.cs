using TABP.Application.DTOs.RoomCategoriesDtos;
using Application.Queries.RoomCategoryQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Handlers.RoomCategoryHandlers;

public class GetRoomCategoriesByHotelIdQueryHandler :IRequestHandler<GetRoomCategoriesByHotelIdQuery,PaginatedList<RoomTypeDto>>
{
    private readonly RoomTypeRepositoryInterface _roomTypeRepository;
    private readonly IMapper _mapper;

    public GetRoomCategoriesByHotelIdQueryHandler(RoomTypeRepositoryInterface roomTypeRepository, IMapper mapper)
    {
        _roomTypeRepository = roomTypeRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoomTypeDto>> Handle(GetRoomCategoriesByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await
            _roomTypeRepository
                .GetAllAsync(
                request.HotelId,
                request.IncludeAmenities,
                request.PageNumber,
                request.PageSize
                );

        return new PaginatedList<RoomTypeDto>(
            _mapper.Map<List<RoomTypeDto>>(paginatedList.Items),
            paginatedList.PageData);
    }
}