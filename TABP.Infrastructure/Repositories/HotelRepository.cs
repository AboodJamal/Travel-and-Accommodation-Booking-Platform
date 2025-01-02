using Infrastructure.Interfaces;
using Infrastructure.ExtraModels;
using TABP.Domain.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.HelperMethods.DiscountMethod;

namespace Infrastructure.Repositories;

public class HotelRepository : HotelRepositoryInterface
{
    private readonly InfrastructureDbContext _context;
    private readonly ILogger<HotelRepository> _logger;

    public HotelRepository(InfrastructureDbContext context, ILogger<HotelRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<PaginatedList<Hotel>> GetAllAsync(string? searchQuery, int pageNum, int pageSize)
    {
        try
        {
            var query = _context.Hotels.AsQueryable();
            var totalItemCount = await query.CountAsync();
            var pageData = new PageData(totalItemCount, pageSize, pageNum);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                query = query.Where
                (city => city.Name.Contains(searchQuery) ||
                         city.Description.Contains(searchQuery) ||
                         city.StreetAddress.Contains(searchQuery)
                );
            }

            var result = query
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .AsNoTracking()
                .ToList();

            return new PaginatedList<Hotel>(result, pageData);
        }
        catch (Exception)
        {
            return new PaginatedList<Hotel>(new List<Hotel>(), new PageData(0, 0, 0));
        }
    }

    public async Task<Hotel?> GetByIdAsync(Guid hotelId)
    {
        try
        {
            return await _context
                .Hotels
                .AsNoTracking()
                .SingleAsync(hotel => hotel.Id.Equals(hotelId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return null;
    }

    public async Task<Hotel?> InsertAsync(Hotel hotel)
    {
        try
        {
            await _context.Hotels.AddAsync(hotel);
            await SaveChangesAsync();
            return hotel;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task UpdateAsync(Hotel hotel)
    {
        try
        {
            _context.Hotels.Update(hotel);
            await SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new ConstraintViolationException("Error updating the hotel. Check for a violation of hotel attributes.");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new InvalidOperationException("Error Occured while updating hotel.");
        }
    }

    public async Task DeleteAsync(Guid hotelId)
    {
        var hotelToRemove = new Hotel { Id = hotelId };
        _context.Hotels.Remove(hotelToRemove);
        await _context.SaveChangesAsync();
    }
    
    private bool IsRoomAvailable(Guid roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        var roomBookings = _context
            .Bookings
            .Where(b => b.RoomId.Equals(roomId))
            .ToList();

        return roomBookings.All(booking => 
            checkInDate.Date > booking.CheckOutDate.Date || 
            checkOutDate.Date < booking.CheckInDate.Date);
    }

    public async Task<List<Room>> GetAllHotelAvailableRoomsAsync(
        Guid hotelId,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
        var rooms = await (from hotel in _context.Hotels
            join roomType in _context.RoomTypes on hotel.Id equals roomType.HotelId
            join room in _context.Rooms on roomType.Id equals room.RoomTypeId
            where roomType.HotelId.Equals(hotelId)
            select room
            ).ToListAsync();

        return rooms.Where(room => 
            IsRoomAvailable(
            room.Id,
            checkInDate,
            checkOutDate)).ToList();
    }
    
    public async Task<PaginatedList<ResultOfHotelSearch>> HotelSearchAsync(ParametersOfHotelSearch searchParams)
    {

        var cityFilterQuery = _context.Cities.AsQueryable();

        if (searchParams.CityName is not null)
        {
            cityFilterQuery = cityFilterQuery
                .Where(city => city
                    .Name.ToLower()
                    .Contains(searchParams.CityName.Trim().ToLower()));
        }
        
        var roomFilterQuery = FindAvailableRoomsWithSpecificCapacity(
            searchParams.Adults,
            searchParams.Children,
            searchParams.CheckInDate,
            searchParams.CheckOutDate);

        var hotelFilterQuery = from hotel in _context.Hotels
            where hotel.Rating >= searchParams.StarRate select hotel;

        var query = from city in cityFilterQuery
            join hotel in hotelFilterQuery on city.Id equals hotel.CityId
            join roomType in _context.RoomTypes on hotel.Id equals roomType.HotelId
            join room in roomFilterQuery on roomType.Id equals room.RoomTypeId
            select new ResultOfHotelSearch
            {
                CityId = city.Id,
                CityName = city.Name,
                HotelId = hotel.Id,
                HotelName = hotel.Name,
                Rating = hotel.Rating,
                RoomId = room.Id,
                RoomPricePerNight = roomType.PricePerNight,
                Discount = DiscountMethod.GetDiscount(roomType.Discounts),
                RoomType = roomType.Category.ToString()
            };
        
        var totalItemCount = await query.CountAsync();
        var pageData = new PageData(totalItemCount, searchParams.PageSize, searchParams.PageNumber);
        
        var result = await query
            .Skip(searchParams.PageSize * (searchParams.PageNumber - 1))
            .Take(searchParams.PageSize)
            .AsNoTracking()
            .ToListAsync();
        
        return new PaginatedList<ResultOfHotelSearch>(result, pageData);
    }

    public async Task<List<FeaturedDeal>> GetAllFeaturedDealsAsync(int count)
    {
        return (await (from city in _context.Cities
            join hotel in _context.Hotels on city.Id equals hotel.CityId
            join roomType in _context.RoomTypes on hotel.Id equals roomType.HotelId
            let activeDiscount = DiscountMethod.GetDiscount(roomType.Discounts)
            select new FeaturedDeal
            {
                CityName = city.Name,
                HotelId = hotel.Id,
                HotelName = hotel.Name,
                HotelRating = hotel.Rating,
                BaseRoomPrice = roomType.PricePerNight,
                RoomClassId = roomType.Id,
                Discount = activeDiscount,
                FinalRoomPrice = roomType.PricePerNight * (1 - activeDiscount)
            }).ToListAsync())
            .OrderByDescending(d => d.Discount)
            .ThenBy(d => d.FinalRoomPrice)
            .Take(count).ToList();
    }

    private IQueryable<Room> FindAvailableRoomsWithSpecificCapacity(int adults, int children, DateTime checkInDate, DateTime checkOutDate)
    {
        return from room in _context.Rooms
            where room.AdultsCapacity == adults &&
                  room.ChildrenCapacity == children &&
                  _context.Bookings.Where(booking => booking.RoomId == room.Id).All
                  (booking => checkInDate.Date > booking.CheckOutDate.Date || 
                  checkOutDate.Date < booking.CheckInDate.Date) select room;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(Guid hotelId)
    {
        return await _context
            .Hotels
            .AnyAsync
            (hotel => hotel.Id.Equals(hotelId));
    }

}


