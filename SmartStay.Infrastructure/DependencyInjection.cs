using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;
using SmartStay.Infrastructure.Persistance.Migrations;

namespace SmartStay.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
     
     services.AddDbContext<ISmartStayDbContext,SmartStayDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("DefaultConnection")));


        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
    
}