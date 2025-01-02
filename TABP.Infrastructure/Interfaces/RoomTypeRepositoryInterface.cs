using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
namespace Infrastructure.Interfaces;

public interface RoomTypeRepositoryInterface
{
    public Task<PaginatedList<RoomType>> 
    GetAllAsync(
        Guid hotelId,
        bool includeAmenities,
        int pageNumber,
        int pageSize);
    public Task<RoomType?> GetByIdAsync(Guid roomTypeId);
    Task<RoomType?> InsertAsync(RoomType roomType);
    Task UpdateAsync(RoomType roomType);
    Task DeleteAsync(Guid roomTypeId);
    Task<bool> IsRoomTypeInHotel(Guid hotelId, Guid roomTypeId);
    Task SaveChangesAsync();
    Task<bool> IsExistAsync(Guid roomTypeId);
}