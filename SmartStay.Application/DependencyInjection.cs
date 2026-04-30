using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartStay.Application.Dto.UserDto;
using SmartStay.Application.Interfaces;
using SmartStay.Application.Mapper;
using SmartStay.Application.Services;
using SmartStay.Application.Util;
using SmartStay.Domain.Entities;

namespace SmartStay.Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
   {
      // services.AddMediaR(cfg =>
      //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
      
      //M.G:no authentication for now
      // services.AddAuthentication(options =>
      //    {
      //       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      //       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      //       options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      //    })
      //    .AddJwtBearer(options =>
      //    {
      //       var jwtKey = configuration["Jwt:Key"] 
      //                    ?? throw new InvalidOperationException("JWT Key is not configured in appsettings.json");
      //    
      //       if (jwtKey.Length < 32)
      //          throw new InvalidOperationException("JWT Key must be at least 32 characters long");
      //
      //       options.TokenValidationParameters = new TokenValidationParameters
      //       {
      //          ValidateIssuer = true,
      //          ValidateAudience = true,
      //          ValidateLifetime = true,
      //          ValidateIssuerSigningKey = true,
      //          ValidIssuer = configuration["Jwt:Issuer"],
      //          ValidAudience = configuration["Jwt:Audience"],
      //          IssuerSigningKey = new SymmetricSecurityKey(
      //             Encoding.UTF8.GetBytes(jwtKey)),
      //          ClockSkew = TimeSpan.Zero 
      //       };
      //
      //    });
      //
      // services.AddAuthorization();
 
      
      services.AddSingleton<IPasswordHasher<string>, PasswordHasherArgon2>()
         .AddSingleton<IMapper<User, UserLoginResponseDto>, UserLoginResponseMapper>()
         .AddSingleton<IMapper<User,UserCreationRequestDto>, UserCreationDtoMapper>()
         .AddSingleton<JwtService>()
         .AddScoped<IAuthenticationService, AuthenticationService>();//M.G: mora bit scoped jer ovisi o dbContextu koj je po default scoped
      
      return services;
   }
}