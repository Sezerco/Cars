
using Cars.Models.DTO;

namespace Cars.BL.Interfaces
{
    public interface IBlCarCustomerService
    {
        IEnumerable<Car> GetCarsByCustomerId(string customerId);
    }
}
