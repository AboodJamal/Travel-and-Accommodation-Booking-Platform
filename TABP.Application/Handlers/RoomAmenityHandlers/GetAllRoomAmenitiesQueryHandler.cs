using TABP.Application.DTOs.RoomAmenityDtos;
using Application.Queries.RoomAmenityQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Handlers.RoomAmenityHandlers;

public class GetAllRoomAmenitiesQueryHandler : IRequestHandler<GetAllRoomAmenitiesQuery, PaginatedList<RoomAmenityDto>>
{
    private readonly RoomAmenityRepositoryInterface _roomAmenityRepository;
    private readonly IMapper _mapper;

    public GetAllRoomAmenitiesQueryHandler(RoomAmenityRepositoryInterface roomAmenityRepository, IMapper mapper)
    {
        _roomAmenityRepository = roomAmenityRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoomAmenityDto>> Handle(GetAllRoomAmenitiesQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await 
            _roomAmenityRepository
                .GetAllAsync(
                    request.SearchQuery,
                    request.PageNumber,
                    request.PageSize);

        return new PaginatedList<RoomAmenityDto>(
            _mapper.Map<List<RoomAmenityDto>>(paginatedList.Items),
            paginatedList.PageData);
    }
}