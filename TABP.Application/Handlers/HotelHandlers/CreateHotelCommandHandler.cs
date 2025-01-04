using Application.Commands.HotelCommands;
using TABP.Application.DTOs.HotelDtos;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.HotelHandlers;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, HotelNoRoomsDto?>
{
    private readonly HotelRepositoryInterface _hotelRepository;
    private readonly IMapper _mapper;

    public CreateHotelCommandHandler(HotelRepositoryInterface hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<HotelNoRoomsDto?> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        var hotelToAdd = _mapper.Map<Hotel>(request);
        var addedHotel = await _hotelRepository.InsertAsync(hotelToAdd);
        return addedHotel is null ? null : _mapper.Map<HotelNoRoomsDto>(addedHotel);
    }
}