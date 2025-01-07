using TABP.Application.DTOs.BookingDtos;
using Application.Queries.UserQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.UserHandlers;

public class GetBookingsForAuthenticatedGuestQueryHandler :
    IRequestHandler<GetBookingsForAuthenticatedGuestQuery, List<BookingDto>>
{
    private readonly UserRepositoryInterface _userRepository;
    private readonly IMapper _mapper;

    public GetBookingsForAuthenticatedGuestQueryHandler(UserRepositoryInterface userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<BookingDto>> Handle(GetBookingsForAuthenticatedGuestQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<BookingDto>>
        (await _userRepository
        .GetBookingsForSpecificAuthenticatedGuestAsync
        (request.Email, request.Count));
    }
}