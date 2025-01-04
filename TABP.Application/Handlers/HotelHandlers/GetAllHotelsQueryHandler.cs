using TABP.Application.DTOs.HotelDtos;
using Application.Queries.HotelQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.HotelHandlers;

public class GetAllHotelsQueryHandler : IRequestHandler<GetAllHotelsQuery, PaginatedList<HotelNoRoomsDto>>
{
    private readonly HotelRepositoryInterface _hotelRepository;
    private readonly IMapper _mapper;

    public GetAllHotelsQueryHandler(HotelRepositoryInterface hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<HotelNoRoomsDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await 
            _hotelRepository
                .GetAllAsync(
                    request.SearchQuery,
                    request.PageNumber,
                    request.PageSize);

        return new PaginatedList<HotelNoRoomsDto>(
            _mapper.Map<List<HotelNoRoomsDto>>(paginatedList.Items),
            paginatedList.PageData);
    }
}