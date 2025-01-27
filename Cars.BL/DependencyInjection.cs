using Microsoft.Extensions.DependencyInjection;
using Cars.BL.Interfaces;
using Cars.BL.Services;

namespace Cars.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddBusinessDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ICarService, CarService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IBlCarCustomerService, BlCarCustomerService>();

            return services;
        }
    }
}
