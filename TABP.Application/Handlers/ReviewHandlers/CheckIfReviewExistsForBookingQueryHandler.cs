using Application.Queries.ReviewQueries;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.ReviewHandlers;

public class CheckIfReviewExistsForBookingQueryHandler : IRequestHandler<CheckIfReviewExistsForBookingQuery, bool>
{
    private readonly ReviewRepositoryInterface _reviewRepository;

    public CheckIfReviewExistsForBookingQueryHandler(ReviewRepositoryInterface reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<bool> Handle(CheckIfReviewExistsForBookingQuery request, CancellationToken cancellationToken)
    {
        return await _reviewRepository.HasBookingReviewAsync(request.BookingId);
    }
}