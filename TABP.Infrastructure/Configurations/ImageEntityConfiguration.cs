using TABP.Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations;

public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder
            .Property(image => image.Format)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<ImgFormat>());

        builder
            .Property(image => image.EntityId)
            .IsRequired();

        builder
            .Property(image => image.Url)
            .IsRequired();
        
        builder
            .Property(image => image.Type)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<ImgType>());
    }
}