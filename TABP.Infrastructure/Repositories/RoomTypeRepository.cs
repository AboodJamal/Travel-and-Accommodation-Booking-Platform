﻿using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class RoomTypeRepository : RoomTypeRepositoryInterface
{
    private readonly InfrastructureDbContext _context;
    private readonly ILogger<RoomTypeRepository> _logger;

    public RoomTypeRepository(InfrastructureDbContext context, ILogger<RoomTypeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PaginatedList<RoomType>> GetAllAsync(Guid hotelId, bool includeAmenities, int pageNum, int pageSize)
    {
        try
        {
            var query = _context
                .RoomTypes
                .Where(roomType => roomType
                    .HotelId
                    .Equals(hotelId))
                .AsQueryable();
            
            var totalItemCount = await query.CountAsync();
            var pageData = new PageData(totalItemCount, pageSize, pageNum);
            
            if (includeAmenities)
            {
                query = query.
                    Include(roomCategory =>
                    roomCategory.Amenities);
            }

            var result = query
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .AsNoTracking()
                .ToList();

            return new PaginatedList<RoomType>(result, pageData);
        }
        catch (Exception)
        {
            return new PaginatedList<RoomType>(
                new List<RoomType>(),
                new PageData(0, 0, 0));
        }
    }

    public async Task<RoomType?> GetByIdAsync(Guid roomTypeId)
    {
        try
        {
            return await _context
                .RoomTypes
                .SingleAsync(roomType => roomType.Id.Equals(roomTypeId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return null;
    }

    public async Task<RoomType?> InsertAsync(RoomType roomType)
    {
        try
        {
            await _context.RoomTypes.AddAsync(roomType);
            await SaveChangesAsync();
            return roomType;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task UpdateAsync(RoomType roomType)
    {
        _context.RoomTypes.Update(roomType);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid roomTypeId)
    {
        var roomTypeToRemove = new RoomType { Id = roomTypeId };
        _context.RoomTypes.Remove(roomTypeToRemove);
        await SaveChangesAsync();
    }

    public async Task<bool> IsRoomTypeInHotel(Guid hotelId, Guid roomTypeId)
    {
        return (await GetByIdAsync(roomTypeId))
               .HotelId.Equals(hotelId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(Guid roomTypeId)
    {
        return await _context
            .RoomTypes
            .AnyAsync
            (roomType => roomType.Id.Equals(roomTypeId));
    }
}