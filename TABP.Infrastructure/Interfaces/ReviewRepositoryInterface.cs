using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
namespace Infrastructure.Interfaces;

public interface ReviewRepositoryInterface
{
    public Task<PaginatedList<Review>> GetAllByHotelIdAsync(Guid hotelId, string? searchQuery, int pageNumber, int pageSize);
    public Task<Review?> GetByIdAsync(Guid reviewId);
    public Task<Review?> InsertAsync(Review review);
    public Task UpdateAsync(Review review);
    public Task DeleteAsync(Guid reviewId);
    public Task<bool> HasBookingReviewAsync(Guid bookingId);
    public Task SaveChangesAsync();
    public Task<bool> IsExistAsync(Guid reviewId);
}