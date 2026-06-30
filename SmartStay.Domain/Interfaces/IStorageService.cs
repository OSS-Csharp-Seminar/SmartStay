namespace SmartStay.Application.Interfaces;

public interface IStorageService
{
    Task<string> UploadAsync(Stream stream, string contentType, string extension);
    Task DeleteAsync(string fileName);
}