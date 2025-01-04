using Application.Queries.CityQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.CityHandlers;

public class CheckIfCityExistsQueryHandler : IRequestHandler<CheckIfCityExistsQuery, bool>
{
    private readonly CityRepositoryInterface _cityRepository;

    public CheckIfCityExistsQueryHandler(CityRepositoryInterface cityRepository)
    {
        _cityRepository = cityRepository;
    }
    
    public async Task<bool> Handle(CheckIfCityExistsQuery request, CancellationToken cancellationToken)
    {
        return await _cityRepository.IsExistAsync(request.Id);
    }
}