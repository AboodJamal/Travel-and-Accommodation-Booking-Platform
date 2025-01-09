using TABP.Application.DTOs.DiscountDtos;
using Application.Queries.DiscountQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.DiscountHandlers;

public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery,DiscountDto>
{
    private readonly DiscountRepositoryInterface _discountRepository;
    private readonly IMapper _mapper;

    public GetDiscountByIdQueryHandler(DiscountRepositoryInterface discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<DiscountDto> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(request.Id);
        return _mapper.Map<DiscountDto>(discount);
    }
}