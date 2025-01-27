using Cars.BL.Interfaces;
using Cars.DL.Interfaces;
using Cars.Models.DTO;


namespace Cars.BL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Email))
            {
                throw new ArgumentException("Customer email is required.");
            }

            _customerRepository.AddCustomer(customer);
        }

        public void DeleteCustomer(string id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            _customerRepository.DeleteCustomer(id);
        }

        public List<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public Customer? GetCustomerById(string id)
        {
            return _customerRepository.GetCustomerById(id);
        }
    }
}
