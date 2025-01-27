using Cars.BL.Interfaces;
using Cars.BL.Services;
using Cars.Models.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Tests
{
    public class BlCarCustomerServiceTest
    {
        private readonly Mock<ICarService> _carServiceMock;
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly List<Car> _cars;
        private readonly List<Customer> _customers;
        private readonly BlCarCustomerService _blCarCustomerService;

        public BlCarCustomerServiceTest()
        {
            _carServiceMock = new Mock<ICarService>();
            _customerServiceMock = new Mock<ICustomerService>();

            _cars = new List<Car>
            {
                new Car { Id = "1", CustomerId = "100", Model = "Toyota", Year = 2020, Price = 25000m },
                new Car { Id = "2", CustomerId = "101", Model = "Honda", Year = 2021, Price = 30000m },
                new Car { Id = "3", CustomerId = "100", Model = "BMW", Year = 2022, Price = 40000m }
            };

            _customers = new List<Customer>
            {
                new Customer { Id = "100", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Customer { Id = "101", FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            };

            _blCarCustomerService = new BlCarCustomerService(_carServiceMock.Object, _customerServiceMock.Object);
        }

        [Fact]
        public void GetCarsByCustomerId_ValidCustomerId_ReturnsCars()
        {
            // Arrange
            var customerId = "100";
            var expectedCars = _cars.Where(c => c.CustomerId == customerId).ToList();

            _customerServiceMock.Setup(service => service.GetCustomerById(customerId)).Returns(_customers.First(c => c.Id == customerId));
            _carServiceMock.Setup(service => service.GetCars()).Returns(_cars);

            // Act
            var result = _blCarCustomerService.GetCarsByCustomerId(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCars.Count, result.Count());
            Assert.All(result, car => Assert.Equal(customerId, car.CustomerId));
        }

        [Fact]
        public void GetCarsByCustomerId_CustomerNotFound_ThrowsArgumentException()
        {
            // Arrange
            var customerId = "999"; 

            _customerServiceMock.Setup(service => service.GetCustomerById(customerId)).Returns((Customer?)null);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _blCarCustomerService.GetCarsByCustomerId(customerId));
            Assert.Equal("Customer not found.", exception.Message);
        }
    }
}
