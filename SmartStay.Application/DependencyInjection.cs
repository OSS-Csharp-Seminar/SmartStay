using Microsoft.Extensions.DependencyInjection;

namespace SmartStay.Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services)
   {
      // services.AddMediaR(cfg =>
      //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
      
      
      return services;
   }
}