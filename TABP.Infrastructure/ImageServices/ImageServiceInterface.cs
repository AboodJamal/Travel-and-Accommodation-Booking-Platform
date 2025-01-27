using Infrastructure.ImageServices.ImageDtos;

namespace Infrastructure.ImageServices;

public interface ImageServiceInterface
{
    public Task UploadImageAsync(ImageCreationDto imageCreationDto);
    public Task UploadThumbnailAsync(ImageCreationDto imageCreationDto);
    public Task<List<string>> GetAllImagesAsync(Guid entityId);
    public Task DeleteImageAsync(Guid entityId, Guid imageId);
}
