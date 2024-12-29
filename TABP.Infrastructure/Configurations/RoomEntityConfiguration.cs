using TABP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder
            .HasOne<RoomType>()
            .WithMany()
            .HasForeignKey(room => room.RoomTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Property(room => room.Rating)
            .IsRequired();

        builder
            .Property(room => room.AdultsCapacity)
            .IsRequired();

        builder
            .Property(room => room.ChildrenCapacity);

        builder
            .Property(room => room.View)
            .HasMaxLength(100);
    }
}   