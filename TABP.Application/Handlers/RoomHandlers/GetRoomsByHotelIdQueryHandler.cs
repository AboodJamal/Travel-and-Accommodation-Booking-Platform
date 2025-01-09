using TABP.Application.DTOs.RoomDtos;
using Application.Queries.RoomQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Handlers.RoomHandlers;

public class GetRoomsByHotelIdQueryHandler : IRequestHandler<GetRoomsByHotelIdQuery, PaginatedList<RoomDto>>
{
    private readonly RoomRepositoryInterface _roomRepository;
    private readonly IMapper _mapper;

    public GetRoomsByHotelIdQueryHandler(RoomRepositoryInterface roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoomDto>> Handle(GetRoomsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await 
            _roomRepository.GetRoomsByHotelIdAsync
            (request.HotelId,
            request.SearchQuery, 
            request.PageNumber,
            request.PageSize);

        return new PaginatedList<RoomDto>(
            _mapper.Map<List<RoomDto>>(paginatedList.Items),
            paginatedList.PageData);
    }
}