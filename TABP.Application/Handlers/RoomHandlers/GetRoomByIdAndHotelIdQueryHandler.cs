using TABP.Application.DTOs.RoomDtos;
using Application.Queries.RoomQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.RoomHandlers;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery,RoomDto?>
{
    private readonly RoomRepositoryInterface _roomRepository;
    private readonly IMapper _mapper;

    public GetRoomByIdQueryHandler(RoomRepositoryInterface roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<RoomDto?> Handle(GetRoomByIdQuery request,
    CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.RoomId);
        return _mapper.Map<RoomDto>(room);
    }
}