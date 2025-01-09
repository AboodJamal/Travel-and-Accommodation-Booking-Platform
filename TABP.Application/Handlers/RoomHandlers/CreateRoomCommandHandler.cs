using Application.Commands.RoomCommands;
using TABP.Application.DTOs.RoomDtos;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using Infrastructure.Exceptions;
using MediatR;

namespace Application.Handlers.RoomHandlers;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomDto?>
{
    private readonly RoomRepositoryInterface _roomRepository;
    private readonly RoomTypeRepositoryInterface _roomTypeRepository;
    private readonly HotelRepositoryInterface _hotelRepository;
    private readonly IMapper _mapper;

    public CreateRoomCommandHandler(RoomRepositoryInterface roomRepository, IMapper mapper, RoomTypeRepositoryInterface roomTypeRepository, HotelRepositoryInterface hotelRepository)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
        _roomTypeRepository = roomTypeRepository;
        _hotelRepository = hotelRepository;
    }

    public async Task<RoomDto?> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        if (!await _hotelRepository.IsExistAsync(request.HotelId))
            throw new NotFoundException($"Hotel with ID: {request.HotelId}");
        
        if (!await _roomTypeRepository.IsExistAsync(request.RoomTypeId))
            throw new NotFoundException($"Room type with ID: {request.RoomTypeId}");
        
        if (!await _roomTypeRepository
            .IsRoomTypeInHotel(request.HotelId, request.RoomTypeId))
            throw new NotFoundException("","The room type you asked for does not exist for the given hotel.");
        
        var roomToAdd = _mapper.Map<Room>(request);
        var addedRoom = await _roomRepository.InsertAsync(roomToAdd);
        return addedRoom is null ? null : _mapper.Map<RoomDto>(addedRoom);
    }
}