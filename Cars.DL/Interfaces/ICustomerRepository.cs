using Cars.Models.DTO;

namespace Cars.DL.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        void AddCustomer(Customer customer);
        void DeleteCustomer(string id);
        Customer? GetCustomerById(string id);
    }
}
