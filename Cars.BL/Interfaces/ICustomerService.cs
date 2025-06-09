using Cars.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.BL.Interfaces
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(string id);
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(string id);
    }
}