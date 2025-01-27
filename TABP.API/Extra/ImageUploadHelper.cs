using Infrastructure.ImageServices.ImageDtos;
using Domain.Enums;

namespace TABP.API.Extra;

public static class ImageUploadHelper
{
    public static async Task<ImageCreationDto> CreateImageCreationDto(Guid entityId, IFormFile file, ImgType type)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var base64Content = Convert.ToBase64String(memoryStream.ToArray());
        var imageFormat = GetImageFormat(file.ContentType);
        if (imageFormat == null)
            throw new NotSupportedException($"The {file.ContentType.Split('/')[1]} format is not supported");

        return new ImageCreationDto
        {
            EntityId = entityId,
            Base64Content = base64Content,
            Format = imageFormat.Value,
            Type = type
        };
    }
    
    private static ImgFormat? GetImageFormat(string contentType)
    {
        return contentType.ToLower() switch
        {
            "image/jpeg" or "image/jpg" => ImgFormat.Jpeg, 
            "image/png" => ImgFormat.Png,
            _ => null
        };
    }
}