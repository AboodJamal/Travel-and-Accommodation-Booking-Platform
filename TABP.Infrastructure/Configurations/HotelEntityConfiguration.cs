using TABP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class HotelEntityConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder
            .Property(hotel => hotel.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(hotel => hotel.Rating)
            .IsRequired();

        builder
            .Property(hotel => hotel.StreetAddress)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(hotel => hotel.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(hotel => hotel.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .Property(hotel => hotel.FloorsNumber)
            .IsRequired();

    }
}

