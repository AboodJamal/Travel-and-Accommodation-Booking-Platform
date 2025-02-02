﻿using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.HelperMethods.DiscountMethod;

namespace Infrastructure.Repositories;

public class RoomRepository : RoomRepositoryInterface
{
    private readonly InfrastructureDbContext _context;
    private readonly ILogger<RoomRepository> _logger;

    public RoomRepository(InfrastructureDbContext context, ILogger<RoomRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    private async Task<PaginatedList<Room>> PaginateQueryAsync(IQueryable<Room> query, int pageNum, int pageSize)
    {
        var totalItemCount = await query.CountAsync();
        var pageData = new PageData(totalItemCount, pageSize, pageNum);

        var result = await query
            .Skip(pageSize * (pageNum - 1))
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PaginatedList<Room>(result, pageData);
    }
    
    public async Task<PaginatedList<Room>> GetAllAsync(string? searchQuery, int pageNum, int pageSize)
    {
        try
        {
            var query = _context.Rooms.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                query = query.Where(room => room.View.Contains(searchQuery));
            }
            return await PaginateQueryAsync(query, pageNum, pageSize);
        }
        catch (Exception)
        {
            return new PaginatedList<Room>(new List<Room>(), new PageData(0, 0, 0));
        }
    }

    public async Task<PaginatedList<Room>> GetRoomsByHotelIdAsync(Guid hotelId, string? searchQuery, int pageNum, int pageSize)
    {
        try
        {
            var query = (from roomType in _context.RoomTypes
                join room in _context.Rooms on roomType.Id equals room.RoomTypeId
                where roomType.HotelId == hotelId
                select room);
            
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                query = query.Where(room => room.View.Contains(searchQuery));
            }

            return await PaginateQueryAsync(query, pageNum, pageSize);
        }
        catch (Exception)
        {
            return new PaginatedList<Room>(new List<Room>(), new PageData(0, 0, 0));
        }
    }

    public async Task<bool> IsRoomInHotelAsync(Guid hotelId, Guid roomId)
    {
        return await (from roomType in _context.RoomTypes
            where roomType.HotelId.Equals(hotelId)
            join room in _context.Rooms on
            roomType.Id equals room.RoomTypeId 
            where room.Id.Equals(roomId) select room)
            .AnyAsync();
    }

    public async Task<Room?> GetByIdAsync(Guid roomId)
    {
        try
        {
            return await _context
                .Rooms
                .SingleAsync(room => room.Id.Equals(roomId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return null;
    }

    public async Task<float> GetPriceForRoomWithDiscount(Guid roomId)
    {
        return await (from room in _context.Rooms
                where room.Id == roomId
                join roomType in _context.RoomTypes on 
                room.RoomTypeId equals roomType.Id
                select roomType.PricePerNight * 
                (1 - DiscountMethod.GetDiscount(roomType.Discounts))
            ).SingleAsync();
    }
    
    public async Task<Room?> InsertAsync(Room room)
    {
        try
        {
            await _context.Rooms.AddAsync(room);
            await SaveChangesAsync();
            return room;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task UpdateAsync(Room room)
    {
        _context.Rooms.Update(room);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid roomId)
    {
        var roomToRemove = new Room { Id = roomId };
        _context.Rooms.Remove(roomToRemove);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(Guid roomId)
    {
        return await _context
            .Rooms
            .AnyAsync
            (room => room.Id.Equals(roomId));
    }
}