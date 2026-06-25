using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using SmartStay.Domain.Enums;

namespace SmartStay.Application.Util;

/// <summary>
/// Class allows access to page only to specific roles
/// </summary>
public static class AuthorizationPolicy
{
   public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
   {
     //M.G: Add row to this method if you want to add new role to app 
      services.AddAuthorization(options =>
      {
         options.AddPolicy(Role.Renter.ToString(), policy => policy.RequireRole(Role.Renter.ToString()));
      });
      return services;
   }
}