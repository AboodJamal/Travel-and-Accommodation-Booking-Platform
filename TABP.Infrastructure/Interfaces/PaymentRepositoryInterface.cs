using TABP.Domain.Entities;
namespace Infrastructure.Interfaces;

public interface PaymentRepositoryInterface
{
    public Task<IReadOnlyList<Payment>> GetAllAsync();
    public Task<Payment?> GetByIdAsync(Guid paymentId);
    Task<Payment?> InsertAsync(Payment payment);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(Guid paymentId);
    Task SaveChangesAsync();
    Task<bool> IsExistAsync(Guid paymentId);
}