using TABP.Application.DTOs.HotelDtos;
using Application.Queries.HotelQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.HotelHandlers;

public class GetFeaturedDealsQueryHandler : IRequestHandler<GetFeaturedDealsQuery, List<FeaturedDealDto>>
{
    private readonly HotelRepositoryInterface _hotelRepository;
    private readonly IMapper _mapper;

    public GetFeaturedDealsQueryHandler(HotelRepositoryInterface hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<List<FeaturedDealDto>> Handle(GetFeaturedDealsQuery request, CancellationToken cancellationToken)
    { 
        return _mapper.Map<List<FeaturedDealDto>>(await _hotelRepository.GetAllFeaturedDealsAsync(request.Count));
    }
}