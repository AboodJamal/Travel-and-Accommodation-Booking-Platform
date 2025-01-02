using TABP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OwnerEntityConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder
            .Property(owner => owner.FirstName)
            .IsRequired()
            .HasMaxLength(25);

        builder
            .Property(owner => owner.LastName)
            .IsRequired()
            .HasMaxLength(25);

        builder
            .Property(hotel => hotel.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder
            .Property(owner => owner.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);
    }
}