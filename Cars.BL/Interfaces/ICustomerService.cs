using Cars.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.BL.Interfaces
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        void DeleteCustomer(string id);
        List<Customer> GetCustomers();
        Customer? GetCustomerById(string id);
    }
}
