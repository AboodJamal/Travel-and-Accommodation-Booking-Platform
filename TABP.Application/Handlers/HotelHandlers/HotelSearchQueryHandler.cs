using Application.Queries.HotelQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.HotelHandlers;

public class HotelSearchQueryHandler : IRequestHandler<HotelSearchQuery, PaginatedList<ResultOfHotelSearch>>
{
    private readonly HotelRepositoryInterface _hotelRepository;
    private readonly IMapper _mapper;

    public HotelSearchQueryHandler(HotelRepositoryInterface hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ResultOfHotelSearch>> Handle(HotelSearchQuery request,
    CancellationToken cancellationToken)
    {
        var hotelSearchParameters = _mapper.Map<ParametersOfHotelSearch>(request);
        return await _hotelRepository.HotelSearchAsync(hotelSearchParameters);
    }
}