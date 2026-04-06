using Microsoft.Extensions.DependencyInjection;
using SmartStay.Application.Dto.UserDto;
using SmartStay.Application.Interfaces;
using SmartStay.Application.Mapper;
using SmartStay.Application.Util;
using SmartStay.Domain.Entities;

namespace SmartStay.Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services)
   {
      // services.AddMediaR(cfg =>
      //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

      services.AddScoped<IPasswordHasher<string>, PasswordHasherArgon2>()
         .AddTransient<IMapper<User,UserLoginResponseDto>, UserLoginResponseMapper>();
      
      return services;
   }
}