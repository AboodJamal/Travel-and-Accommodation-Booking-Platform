using TABP.Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Configurations;


namespace Infrastructure;

public class InfrastructureDbContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<RoomAmenity> RoomAmenities { get; set; }
    public DbSet<Discount> Discounts { get; set; }

    public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CityEntityConfiguration());
        modelBuilder.ApplyConfiguration(new HotelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ImageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OwnerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoomEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());


        modelBuilder.SeedTables();
        base.OnModelCreating(modelBuilder);
    }
    
}