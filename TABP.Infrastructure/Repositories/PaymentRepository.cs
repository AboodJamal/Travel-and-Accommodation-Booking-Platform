using Infrastructure.Interfaces;
using TABP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class PaymentRepository : PaymentRepositoryInterface
{
    private readonly InfrastructureDbContext _context;
    private readonly ILogger _logger;

    public PaymentRepository(InfrastructureDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Payment>> GetAllAsync()
    {
        try
        {
            return await _context
                .Payments
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception)
        {
            return Array.Empty<Payment>();
        }
    }

    public async Task<Payment?> GetByIdAsync(Guid paymentId)
    {
        try
        {
            return await _context
                .Payments
                .SingleAsync(payment => payment.Id.Equals(paymentId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return null;
    }

    public async Task<Payment?> InsertAsync(Payment payment)
    {
        try
        {
            await _context.Payments.AddAsync(payment);
            await SaveChangesAsync();
            return payment;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task UpdateAsync(Payment payment)
    {
        _context.Payments.Update(payment);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid paymentId)
    {
        var paymentToRemove = new Payment { Id = paymentId };
        _context.Payments.Remove(paymentToRemove);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(Guid paymentId)
    {
        return await _context
            .Payments
            .AnyAsync
            (payment => payment.Id.Equals(paymentId));
    }
}