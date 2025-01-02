using MediatR;

namespace Application.Queries.DiscountQueries;

public record CheckIfDiscountExistsQuery : IRequest<bool>
{
    public Guid Id { get; set; }
}