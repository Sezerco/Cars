using Microsoft.Extensions.DependencyInjection;
using Cars.DL.Interfaces;
using Cars.DL.Repositories;

namespace Cars.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddDataDependencies(
                this IServiceCollection services)
        {
            services.AddSingleton<ICarRepository, CarRepository>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
