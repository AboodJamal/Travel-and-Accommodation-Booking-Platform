using TABP.Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations;

public class RoomTypeEntityConfiguration: IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.HasIndex(roomType => roomType.Category);
        
        builder.Property(roomType => roomType.Category)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<RoomCategory>());

        builder
            .Property(roomType => roomType.Category)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(roomType => roomType.PricePerNight)
            .IsRequired();
    }
}