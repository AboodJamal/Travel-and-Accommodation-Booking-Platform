using TABP.Domain.Enums;

namespace TABP.Application.DTOs.ImageDtos;

public record ImageCreationDto
{
    public Guid EntityId { get; set; }
    public string Base64Content { get; set; }
    public ImgFormat Format { get; set; }
    public ImgType Type { get; set; }
}

public enum ImgFormat
{
    Png,
    Jpeg,
    Jpg
}

public enum ImgType
{
    Thumbnail,
    Gallery
}