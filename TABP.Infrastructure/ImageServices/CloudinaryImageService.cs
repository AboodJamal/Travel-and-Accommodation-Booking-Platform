using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TABP.Domain.Entities;
using Domain.Enums;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Infrastructure.ImageServices.ImageDtos;

namespace Infrastructure.ImageServices;

public class CloudinaryImageService : ImageServiceInterface
{
    private readonly InfrastructureDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CloudinaryImageService> _logger;
    private readonly Cloudinary _cloudinary;

    public CloudinaryImageService(InfrastructureDbContext context, IConfiguration configuration, ILogger<CloudinaryImageService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;

        var account = new Account(
            _configuration["Cloudinary:CloudName"],
            _configuration["Cloudinary:ApiKey"],
            _configuration["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    private async Task AddImageAsync(Image image)
    {
        await _context.Images.AddAsync(image);
        await _context.SaveChangesAsync();
    }

    public async Task UploadImageAsync(ImageCreationDto imageCreationDto)
    {
        await UploadImageAsyncInternal(imageCreationDto);
    }

    public async Task UploadThumbnailAsync(ImageCreationDto imageCreationDto)
    {
        var thumbnail = await _context
            .Images
            .SingleOrDefaultAsync(e => e.Type == ImgType.Thumbnail && e.EntityId.Equals(imageCreationDto.EntityId));

        if (thumbnail is not null)
        {
            await DeleteImageAsync(imageCreationDto.EntityId, thumbnail.Id);
            await UploadImageAsyncInternal(imageCreationDto, thumbnail.Id);
        }
        else
        {
            await UploadImageAsync(imageCreationDto);
        }
    }

    private async Task UploadImageAsyncInternal(ImageCreationDto imageCreationDto, Guid? id = null)
    {
        var image = new Image
        {
            Id = id ?? Guid.NewGuid(),
            Format = imageCreationDto.Format,
            EntityId = imageCreationDto.EntityId,
            Type = imageCreationDto.Type
        };

        var formatToUse = image.Format;
        var imageBytes = Convert.FromBase64String(imageCreationDto.Base64Content);

        using var uploadStream = new MemoryStream(imageBytes);
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(image.Id.ToString(), uploadStream),
            PublicId = image.Id.ToString(),
            Folder = _configuration["Cloudinary:FolderName"],
            Format = formatToUse.ToString().ToLower()
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new InvalidOperationException("Failed to upload image to Cloudinary.");
        }

        image.Url = uploadResult.SecureUrl.ToString();

        if (id is not null)
        {
            var existingImage = await _context.Images.FindAsync(id);
            existingImage.Url = image.Url;
            existingImage.Format = image.Format;
        }
        else
        {
            await AddImageAsync(image);
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Image uploaded successfully to Cloudinary.");
    }

    public async Task<List<string>> GetAllImagesAsync(Guid entityId)
    {
        try
        {
            return await _context
                .Images
                .Where(image => image.EntityId.Equals(entityId))
                .Select(image => image.Url)
                .ToListAsync();
        }
        catch (Exception)
        {
            return new List<string>();
        }
    }

    public async Task DeleteImageAsync(Guid entityId, Guid imageId)
    {
        try
        {
            var image = await _context.Images
                .FirstOrDefaultAsync(e => e.Id == imageId && e.EntityId == entityId);

            if (image == null)
                throw new NotFoundException($"Image with ID {imageId} not found");

            var deletionParams = new DeletionParams(image.Id.ToString())
            {
                ResourceType = ResourceType.Image
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result != "ok")
                throw new InvalidOperationException($"Failed to delete image with ID {imageId} from Cloudinary.");

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Image with ID {imageId} deleted successfully from Cloudinary.");
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error deleting image with ID {imageId}: {ex.Message}");
        }
    }
}
