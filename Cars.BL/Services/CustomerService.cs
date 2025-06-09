using Cars.BL.Interfaces;
using Cars.DL.Interfaces;
using Cars.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.BL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Email))
            {
                throw new ArgumentException("Customer email is required.");
            }

            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            await _customerRepository.DeleteCustomerAsync(id);
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetCustomersAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(string id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }
    }
}