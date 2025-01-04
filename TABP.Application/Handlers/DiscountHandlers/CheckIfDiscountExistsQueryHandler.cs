using Application.Queries.DiscountQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.DiscountHandlers;

public class CheckIfDiscountExistsQueryHandler : IRequestHandler<CheckIfDiscountExistsQuery, bool>
{
    private readonly DiscountRepositoryInterface _discountRepository;

    public CheckIfDiscountExistsQueryHandler(DiscountRepositoryInterface discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<bool> Handle(CheckIfDiscountExistsQuery request, CancellationToken cancellationToken)
    {
        return await _discountRepository.IsExistAsync(request.Id);
    }
}