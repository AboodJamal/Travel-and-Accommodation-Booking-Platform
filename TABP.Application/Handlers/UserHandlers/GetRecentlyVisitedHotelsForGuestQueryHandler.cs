using TABP.Application.DTOs.HotelDtos;
using Application.Queries.UserQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Exceptions;
using MediatR;

namespace Application.Handlers.UserHandlers;

public class GetRecentlyVisitedHotelsForGuestQueryHandler :
    IRequestHandler<GetRecentlyVisitedHotelsForGuestQuery, List<HotelNoRoomsDto>>
{
    private readonly UserRepositoryInterface _userRepository;
    private readonly IMapper _mapper;

    public GetRecentlyVisitedHotelsForGuestQueryHandler(UserRepositoryInterface userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<HotelNoRoomsDto>> Handle(GetRecentlyVisitedHotelsForGuestQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsExistAsync(request.GuestId))
        {
            throw new NotFoundException($"User With ID {request.GuestId} Doesn't Exists.");
        }
        
        return _mapper.Map<List<HotelNoRoomsDto>>
        (await _userRepository
        .GetRecentlyVisitedHotelsForSpecificGuestAsync
        (request.GuestId, request.Count));
    }
}