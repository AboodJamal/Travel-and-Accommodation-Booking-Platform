using TABP.Domain.Entities;
namespace Infrastructure.Interfaces;

public interface UserRepositoryInterface
{
    public Task InsertAsync(User user);
    public Task<IReadOnlyList<User>> GetAllAsync();
    public Task<User?> GetByIdAsync(Guid userId);
    public Task UpdateAsync(User user);
    public Task DeleteAsync(Guid userId);
    public Task<Guid?> GetGuestIdByEmailAsync(string email);
    public Task<List<Hotel>> GetRecentlyVisitedHotelsForSpecificGuestAsync(Guid? guestId, int count);
    public Task<List<Hotel>> GetRecentlyVisitedHotelsForSpecificAuthenticatedGuestAsync(string email, int count);
    public Task<List<Booking>> GetBookingsForSpecificAuthenticatedGuestAsync(string email, int count);
    public Task SaveChangesAsync();
    public Task<bool> IsExistAsync(Guid userId);

}