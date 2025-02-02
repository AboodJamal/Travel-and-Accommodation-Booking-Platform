﻿using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class CityRepository : CityRepositoryInterface
{
    private readonly InfrastructureDbContext _context;
    private readonly ILogger<CityRepository> _logger;

    public CityRepository(InfrastructureDbContext context, ILogger<CityRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PaginatedList<City>> GetAllAsync(bool withHotels, string? searchQuery, int pageNum, int pageSize)
    {
        try
        {
            var query = _context.Cities.AsQueryable();
            var totalItemCount = await query.CountAsync();
            var pageData = new PageData(totalItemCount, pageSize, pageNum);
            
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                query = query.Where
                        (city => city.Name.Contains(searchQuery) ||
                        city.CountryName.Contains(searchQuery));
            }
            
            if (withHotels)
            {
                query = query.Include(city => city.Hotels);
            }

            var result = query
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .AsNoTracking()
                .ToList();

            return new PaginatedList<City>(result, pageData);
        }
        catch (Exception)
        {
            return new PaginatedList<City>(
                new List<City>(),
                new PageData(0, 0, 0));
        }
    }

    public async Task<City?> GetByIdAsync(Guid cityId, bool withHotels)
    {
        try
        {
            var query = _context
                .Cities
                .AsQueryable();

            if (withHotels)
            {
                query = query.Include(city => city.Hotels);
            }

            return await query
                .AsNoTracking()
                .SingleAsync
                (city => city.Id.Equals(cityId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return null;
    }

    public async Task<City?> InsertAsync(City city)
    {
        try
        {
            await _context.Cities.AddAsync(city);
            await SaveChangesAsync();
            return city;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task UpdateAsync(City city)
    {
        try
        {
            _context.Cities.Update(city);
            await SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new ConstraintViolationException("Error updating the city.");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new InvalidOperationException("Error Occured while updating city.");
        }
    }
    
    public async Task DeleteAsync(Guid cityId)
    {
        var cityToRemove = new City { Id = cityId };
        _context.Cities.Remove(cityToRemove);
        await SaveChangesAsync();
    }

    public async Task<List<City>> GetTrendingCitiesAsync(int count)
    {
        var trendingCitiesId = await (from booking in _context.Bookings
                join room in _context.Rooms on booking.RoomId equals room.Id
                join roomType in _context.RoomTypes on room.RoomTypeId equals roomType.Id
                join hotel in _context.Hotels on roomType.HotelId equals hotel.Id
                join city in _context.Cities on hotel.CityId equals city.Id
                group city by city.Id into groupedCities
                orderby groupedCities.Count() descending
                select groupedCities.Key)
            .Take(count)
            .ToListAsync();
        
        var trendingCities = await _context.Cities
            .Where(city => trendingCitiesId.Contains(city.Id))
            .ToListAsync();
        
        return trendingCities.OrderBy(city => trendingCitiesId.IndexOf(city.Id)).ToList();
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(Guid cityId)
    {
        return await _context
            .Cities
            .AnyAsync
            (city => city.Id.Equals(cityId));
    }
}