using TABP.Application.DTOs.HotelDtos;
using Application.Queries.UserQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.UserHandlers;

public class GetRecentlyVisitedHotelsForAuthenticatedGuestQueryHandler :IRequestHandler<GetRecentlyVisitedHotelsForAuthenticatedGuestQuery, List<HotelNoRoomsDto>>
{
    private readonly UserRepositoryInterface _userRepository;
    private readonly IMapper _mapper;

    public GetRecentlyVisitedHotelsForAuthenticatedGuestQueryHandler(UserRepositoryInterface userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<HotelNoRoomsDto>> Handle(GetRecentlyVisitedHotelsForAuthenticatedGuestQuery request,
        CancellationToken cancellationToken)
    {
        return _mapper.Map<List<HotelNoRoomsDto>>
        (await _userRepository
        .GetRecentlyVisitedHotelsForSpecificAuthenticatedGuestAsync
        (request.Email, request.Count));
    }
}