using Cars.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.BL.Interfaces
{
    public interface IBlCarCustomerService
    {
        Task<IEnumerable<Car>> GetCarsByCustomerIdAsync(string customerId);
    }
}