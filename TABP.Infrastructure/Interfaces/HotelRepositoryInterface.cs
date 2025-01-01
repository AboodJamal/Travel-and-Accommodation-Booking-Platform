using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
namespace Infrastructure.Interfaces;

public interface HotelRepositoryInterface
{
    public Task<PaginatedList<Hotel>> 
    GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
    public Task<Hotel?> GetByIdAsync(Guid hotelId);
    public Task<Hotel?> InsertAsync(Hotel hotel);
    public Task UpdateAsync(Hotel hotel);
    public Task DeleteAsync(Guid hotelId);
    public Task<List<Room>> GetAllHotelAvailableRoomsAsync(Guid hotelId,
        DateTime checkInDate,
        DateTime checkOutDate);
    public Task<PaginatedList<ResultOfHotelSearch>> HotelSearchAsync(ParametersOfHotelSearch searchParams);
    public Task<List<FeaturedDeal>> GetAllFeaturedDealsAsync(int count);
    public Task SaveChangesAsync();
    public Task<bool> IsExistAsync(Guid hotelId);
}