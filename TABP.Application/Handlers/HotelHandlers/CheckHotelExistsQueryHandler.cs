using Application.Queries.HotelQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.HotelHandlers;

public class CheckIfHotelExistsQueryHandler : IRequestHandler<CheckIfHotelExistsQuery, bool>
{
    private readonly HotelRepositoryInterface _hotelRepository;

    public CheckIfHotelExistsQueryHandler(HotelRepositoryInterface hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }
    
    public async Task<bool> Handle(CheckIfHotelExistsQuery request, CancellationToken cancellationToken)
    {
        return await _hotelRepository.IsExistAsync(request.Id);
    }
}