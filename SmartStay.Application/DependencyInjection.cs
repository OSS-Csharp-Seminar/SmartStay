using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartStay.Application.Dto;
using SmartStay.Application.Dto.BookingDto;
using SmartStay.Application.Dto.RoomDto;
using SmartStay.Application.Dto.UserDto;
using SmartStay.Application.Interfaces;
using SmartStay.Application.Mapper;
using SmartStay.Application.Services;
using SmartStay.Application.Services.AI;
using SmartStay.Application.Services.Authentication;
using SmartStay.Application.Services.Recommendation;
using SmartStay.Application.Services;
using SmartStay.Application.Util;
using SmartStay.Domain.Entities;

namespace SmartStay.Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
   {
       services.AddAuthentication(options =>
          {
             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(options =>
          {
             options.Events = new JwtBearerEvents//M.G: tells autorization where to find token
             {
                OnMessageReceived = context =>
                {
                   context.Token = context.Request.Cookies["token"];
                   return Task.CompletedTask;
                }
             };
           
             options.TokenValidationParameters = new TokenValidationParameters
             {
                //M.G:key, Issuser,audience hardcoded for now. hardcoded in JwtService as well
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer ="SmartStay" ,
                ValidAudience ="SmartStay" ,
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("a9F3kLm2Xq7ZpR8vT1nW6cB4yH0uD5Js")),
                ClockSkew = TimeSpan.Zero,
                 RoleClaimType = ClaimTypes.Role 
             };
      
          });
   
       services.AddBlazoredLocalStorage();
   
       services.AddSingleton<IPasswordHasher<string>, PasswordHasherArgon2>()
          .AddSingleton<IMapper<User, UserCreationRequestDto>, UserCreationDtoMapper>()
          .AddSingleton<IMapper<Room,RoomResponseDto>,RoomMapper>()
          .AddSingleton<IMapper<Review,ReviewResponseDto>,ReviewMapper>()
          .AddSingleton<IMapper<Booking,RoomAvailabilityDto>,RoomAvailabilityMapper>()
          .AddSingleton<JwtSecurityTokenHandler>()
          .AddScoped<JwtService>()
          .AddScoped<IAuthenticationService,
             AuthenticationService>() //M.G: mora bit scoped jer ovisi o dbContextu koj je po default scoped
          .AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>()
          .AddScoped<IBookingService, BookingService>()
          .AddScoped<IRoomService, RoomService>()
          .AddScoped<IReviewService, ReviewService>()
          .AddScoped<ICustomAuthenticationStateProvider>(provider =>//M.G: because AuthenticationStateProvider doesn't have methods from IcustomstateProvider we cast it with this line (and we have to use him) and we have it in CustomStateProvider.
             (CustomAuthenticationStateProvider)provider.GetRequiredService<AuthenticationStateProvider>());
   
      services.AddAppAuthorization();
      
      services.AddHttpClient<IAiService, OllamaService>();
      services.AddScoped<IRecommendationService, RecommendationService>();

      return services;
   }
}