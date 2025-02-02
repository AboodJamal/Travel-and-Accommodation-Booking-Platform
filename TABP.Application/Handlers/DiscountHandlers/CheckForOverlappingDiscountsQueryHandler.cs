﻿using Application.Queries.DiscountQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.DiscountHandlers;

public class CheckForOverlappingDiscountsQueryHandler : IRequestHandler<CheckForOverlappingDiscountQuery,bool>
{
    private readonly DiscountRepositoryInterface _discountRepository;

    public CheckForOverlappingDiscountsQueryHandler(DiscountRepositoryInterface discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public Task<bool> Handle(CheckForOverlappingDiscountQuery request, CancellationToken cancellationToken)
    {
        return _discountRepository.HasOverlappingDiscountAsync(request.RoomTypeId,
                   request.FromDate,
                   request.ToDate);
    }
}