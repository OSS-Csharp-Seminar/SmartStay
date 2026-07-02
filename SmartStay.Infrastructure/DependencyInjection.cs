using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Interfaces;
using SmartStay.Domain.Interfaces.Repository;
using SmartStay.Infrastructure.Persistance;
using SmartStay.Infrastructure.Persistance.Migrations;
using SmartStay.Infrastructure.Repositories;
using SmartStay.Infrastructure.Services;
using SmartStay.Infrastructure.Services.Storage;

namespace SmartStay.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
     
     services.AddDbContext<ISmartStayDbContext,SmartStayDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

     services.AddScoped<IUserRepository, UserRepository>()
         .AddScoped<IBookingRepository, BookingRepository>()
         .AddScoped<IReviewRepository, ReviewRepository>()
         .AddScoped<IRoomRepository, RoomRepository>()
         .AddScoped<IPaymentRepository, PaymentRepository>()
         .AddScoped<IAmenitiesRepository, AmenitiesRepository>()
         .AddScoped<ICancellationLogRepository, CancellationLogRepository>()
         .AddScoped<ITransactionSecurity, TransactionSecurity>()
         .AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>()
         .AddScoped<IEmailService, SmtpEmailService>()
         .AddScoped<IStorageService, CloudflareR2Service>()
         .AddScoped<IRoomImageRepository, RoomImageRepository>()
         .AddScoped<ILocationRepository, LocationRepository>()
         .AddSingleton<IAmazonS3>(sp =>
         {
             var s3Config = new AmazonS3Config
             {
                 ServiceURL = $"https://{config["Cloudflare:AccountId"]}.r2.cloudflarestorage.com",
                 ForcePathStyle = true,
                 HttpClientFactory = new CloudflareHttpClientFactory()
             };
             return new AmazonS3Client(config["Cloudflare:AccessKey"], config["Cloudflare:SecretKey"], s3Config);
         });

        return services;
    }
    
}