using TABP.Domain.Entities;
namespace Infrastructure.Interfaces;

public interface OwnerRepositoryInterface
{
    public Task<IReadOnlyList<Owner>> GetAllAsync();
    public Task<Owner?> GetByIdAsync(Guid ownerId);
    Task<Owner?> InsertAsync(Owner owner);
    Task UpdateAsync(Owner owner);
    Task DeleteAsync(Guid ownerId);
    Task SaveChangesAsync();
    Task<bool> IsExistAsync(Guid ownerId);
}