using Application.Commands.DiscountCommands;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.DiscountHandlers;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand>
{
    private readonly DiscountRepositoryInterface _discountRepository;

    public DeleteDiscountCommandHandler(DiscountRepositoryInterface discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        await _discountRepository.DeleteAsync(request.Id);
    }
}