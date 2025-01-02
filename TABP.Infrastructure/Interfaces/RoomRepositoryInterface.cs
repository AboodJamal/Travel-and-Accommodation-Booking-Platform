using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
namespace Infrastructure.Interfaces;

public interface RoomRepositoryInterface
{
    public Task<PaginatedList<Room>> 
    GetAllAsync(string? searchQuery,
        int pageNumber,
        int pageSize);
    public Task<PaginatedList<Room>> 
    GetRoomsByHotelIdAsync(Guid hotelId,
        string? searchQuery,
        int pageNumber,
        int pageSize);
    public Task<bool> IsRoomInHotelAsync(Guid hotelId,
        Guid roomId);
    public Task<Room?> GetByIdAsync(Guid roomId);
    public Task<float> GetPriceForRoomWithDiscount(Guid roomId);
    public Task<Room?> InsertAsync(Room room);
    public Task UpdateAsync(Room room);
    public Task DeleteAsync(Guid roomId);
    public Task SaveChangesAsync();
    public Task<bool> IsExistAsync(Guid roomId);
}