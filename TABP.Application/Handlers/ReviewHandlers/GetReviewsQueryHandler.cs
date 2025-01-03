using TABP.Application.DTOs.ReviewsDtos;
using Application.Queries.ReviewQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using MediatR;

namespace Application.Handlers.ReviewHandlers;

public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, PaginatedList<ReviewDto>>
{
    private readonly ReviewRepositoryInterface _reviewRepository;
    private readonly IMapper _mapper;

    public GetReviewsQueryHandler(ReviewRepositoryInterface reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ReviewDto>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await 
            _reviewRepository
                .GetAllByHotelIdAsync(
                    request.HotelId,
                    request.SearchQuery,
                    request.PageNumber,
                    request.PageSize);

        return new PaginatedList<ReviewDto>(
            _mapper.Map<List<ReviewDto>>(paginatedList.Items),
            paginatedList.PageData);
    }
}