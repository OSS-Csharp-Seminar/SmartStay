using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using SmartStay.Application.Interfaces;

namespace SmartStay.Infrastructure.Services.Storage;

public class CloudflareR2Service : IStorageService
{
    private readonly IAmazonS3 _s3;
    private readonly IConfiguration _configuration;
    private readonly string? _bucket ;

     public CloudflareR2Service(IAmazonS3 s3, IConfiguration configuration)
     {
         _s3 = s3;
         _configuration = configuration;
         _bucket = _configuration["Cloudflare:BucketName"];
     }
    
    
    public async Task<string> UploadAsync(Stream stream, string contentType, string extension)
    {
        var fileName = $"{Guid.NewGuid()}{extension}";

        var request = new PutObjectRequest
        {
            BucketName = _bucket,
            Key = fileName,
            InputStream = stream,
            ContentType = contentType,
            DisablePayloadSigning = true
        };
        
        await _s3.PutObjectAsync(request);
        return fileName;
    }

    public async Task DeleteAsync(string fileName)
    {
       await _s3.DeleteObjectAsync(_bucket,fileName); 
    }
}