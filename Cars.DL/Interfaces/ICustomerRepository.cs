using Cars.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.DL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(string id);
        Task<Customer?> GetCustomerByIdAsync(string id);
    }
}