using Microsoft.AspNetCore.Http;

namespace SmartStay.Application.Interfaces;

public interface IRoomImageService
{
    Task<List<string>> UploadImagesAsync(Guid roomId, List<IFormFile> files);
    Task DeleteImageAsync(Guid imageId);
}