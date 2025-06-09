using Cars.BL.Interfaces;
using Cars.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public async Task<IEnumerable<Car>> GetCarsByCustomerIdAsync(string customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            var cars = await _carService.GetCarsAsync();
            return cars.Where(car => car.CustomerId == customerId);
        }
    }
}
