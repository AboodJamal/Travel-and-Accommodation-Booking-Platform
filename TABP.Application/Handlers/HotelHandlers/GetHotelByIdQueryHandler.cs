using TABP.Application.DTOs.HotelDtos;
using Application.Queries.HotelQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.HotelHandlers;

public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery,HotelNoRoomsDto?>
{
    private readonly HotelRepositoryInterface _hotelRepository;
    private readonly IMapper _mapper;

    public GetHotelByIdQueryHandler(HotelRepositoryInterface hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<HotelNoRoomsDto?> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id);
        return _mapper.Map<HotelNoRoomsDto>(hotel);
    }
}