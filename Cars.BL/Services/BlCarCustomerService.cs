using Cars.BL.Interfaces;
using Cars.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.BL.Services
{
    public class BlCarCustomerService : IBlCarCustomerService
    {
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;

        public BlCarCustomerService(ICarService carService, ICustomerService customerService)
        {
            _carService = carService;
            _customerService = customerService;
        }
        public IEnumerable<Car> GetCarsByCustomerId(string customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            return _carService.GetCars().Where(car => car.CustomerId == customerId);
        }
    }
}
