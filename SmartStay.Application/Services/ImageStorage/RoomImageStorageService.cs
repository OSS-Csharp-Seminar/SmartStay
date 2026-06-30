using Microsoft.AspNetCore.Http;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;
using SmartStay.Domain.Interfaces.Repository;

namespace SmartStay.Application.Services.ImageStorage;

public class RoomImageStorageService : IRoomImageService
{
    
   private readonly IStorageService _storageService;
   private readonly IRoomImageRepository _repository;

   public RoomImageStorageService(IStorageService storageService, IRoomImageRepository repository)
   {
       _storageService = storageService;
       _repository = repository;
   }
   
    public async Task<List<string>> UploadImagesAsync(Guid roomId, List<IFormFile> files)
    {
        uint maxFileSize = 5 * 1024 * 1024; //M.G: 5MB limit
       var allowed = new[] { ".jpg", ".jpeg", ".png"};
       var images= new List<RoomImage>();
       
       foreach (var file in files)
       {
           var ext = Path.GetExtension(file.FileName).ToLower();

           if (!allowed.Contains(ext))
               continue;
           
           if (file.Length > maxFileSize)
               continue;
           
           using var stream =file.OpenReadStream();
           var fileName = await _storageService.UploadAsync(stream, file.ContentType, ext);
           
          images.Add(new RoomImage{RoomId = roomId, FileName = fileName});
       }
       
       await _repository.AddMultipleAsync(images);

       return images.Select(i => i.FileName).ToList();
    }

    public async Task DeleteImageAsync(Guid imageId)
    {
        var image = await _repository.GetByIdAsync(imageId);
        if (image == null) return;
        
        await _storageService.DeleteAsync(image.FileName);
        _repository.DeleteAsync(image);
    }
}