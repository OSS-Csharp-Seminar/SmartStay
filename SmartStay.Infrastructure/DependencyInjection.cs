using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;
using SmartStay.Infrastructure.Persistance.Migrations;
using SmartStay.Infrastructure.Repositories;
using SmartStay.Infrastructure.Services;

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
         .AddScoped<IEmailService, SmtpEmailService>();

        return services;
    }
    
}