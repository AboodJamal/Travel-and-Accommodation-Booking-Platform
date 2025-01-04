using Application.Commands.DiscountCommands;
using TABP.Application.DTOs.DiscountDtos;
using AutoMapper;
using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using MediatR;

namespace Application.Handlers.DiscountHandlers;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, DiscountDto?>
{
    private readonly DiscountRepositoryInterface _discountRepository;
    private readonly IMapper _mapper;

    public CreateDiscountCommandHandler(DiscountRepositoryInterface discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<DiscountDto?> Handle(CreateDiscountCommand request,
    CancellationToken cancellationToken)
    {
        var discountToAdd = _mapper.Map<Discount>(request);
        var addedDiscount = await _discountRepository.InsertAsync(discountToAdd);
        return _mapper.Map<DiscountDto>(addedDiscount);
    }
}