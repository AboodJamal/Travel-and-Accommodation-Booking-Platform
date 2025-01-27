using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
namespace Infrastructure.Interfaces;

public interface RoomAmenityRepositoryInterface
{
    public Task<PaginatedList<RoomAmenity>>GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
    public Task<RoomAmenity?> GetByIdAsync(Guid amenityId);
    public Task<RoomAmenity?> InsertAsync(RoomAmenity roomAmenity);
    public Task UpdateAsync(RoomAmenity roomAmenity);
    public Task DeleteAsync(Guid amenityId);
    public Task SaveChangesAsync();
    public Task<bool> IsExistAsync(Guid amenityId);
}