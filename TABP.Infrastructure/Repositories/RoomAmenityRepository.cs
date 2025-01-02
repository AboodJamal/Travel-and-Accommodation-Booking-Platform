using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class RoomAmenityRepository : RoomAmenityRepositoryInterface
{
    private readonly InfrastructureDbContext _context;
    private readonly ILogger<RoomAmenityRepository> _logger;

    public RoomAmenityRepository(InfrastructureDbContext context, ILogger<RoomAmenityRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PaginatedList<RoomAmenity>> GetAllAsync(string? searchQuery, int pageNum, int pageSize)
    {
        try
        {
            var query = _context.RoomAmenities.AsQueryable();
            var totalItemCount = await query.CountAsync();
            var pageData = new PageData(totalItemCount, pageSize, pageNum);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                query = query.Where
                (city => city.Name.Contains(searchQuery) || 
                city.Description.Contains(searchQuery));
            }

            var result = query
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .AsNoTracking()
                .ToList();

            return new PaginatedList<RoomAmenity>(result, pageData);
        }
        catch (Exception)
        {
            return new PaginatedList<RoomAmenity>(new List<RoomAmenity>(), new PageData(0, 0, 0));
        }
    }

    public async Task<RoomAmenity?> GetByIdAsync(Guid amenityId)
    {
        try
        {
            var query = _context
                .RoomAmenities
                .AsQueryable();
            
            return await query
                .AsNoTracking()
                .SingleAsync
                    (amenity => amenity.Id.Equals(amenityId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return null;
    }

    public async Task<RoomAmenity?> InsertAsync(RoomAmenity roomAmenity)
    {
        try
        {
            await _context.RoomAmenities.AddAsync(roomAmenity);
            await SaveChangesAsync();
            return roomAmenity;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task UpdateAsync(RoomAmenity roomAmenity)
    {
        try
        {
            _context.RoomAmenities.Update(roomAmenity);
            await SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new ConstraintViolationException("Error updating the roomAmenity. Check for a violation of roomAmenity attributes.");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new InvalidOperationException("Error Occured while updating roomAmenity.");
        }
    }

    public async Task DeleteAsync(Guid amenityId)
    {
        var amenityToRemove = new RoomAmenity { Id = amenityId };
        _context.RoomAmenities.Remove(amenityToRemove);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(Guid amenityId)
    {
        return await _context
            .RoomAmenities
            .AnyAsync
            (roomAmenity => roomAmenity.Id.Equals(amenityId));
    }
}