using Mapster;
using Cars.Models.DTO;
using Cars.Models.Requests;

namespace Cars.MapsterConfig
{
    public class MapsterConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<CarRequest, Car>.NewConfig();
            TypeAdapterConfig<CustomerRequest, Customer>.NewConfig();
        }
    }
}
