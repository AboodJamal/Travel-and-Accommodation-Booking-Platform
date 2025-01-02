using MediatR;

namespace Application.Queries.CityQueries;

public class CheckIfCityExistsQuery : IRequest<bool>
{
    public Guid Id { get; set; }
}