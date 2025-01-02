using TABP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ReviewEntityConfiguration: IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder
            .Property(review => review.Comment)
            .HasMaxLength(300);

        builder
            .Property(review => review.Rating)
            .IsRequired();

    }
}