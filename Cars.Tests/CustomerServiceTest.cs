using Cars.BL.Services;
using Cars.DL.Interfaces;
using Cars.Models.DTO;
using Moq;

namespace Cars.Tests
{
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly List<Customer> _customers;

        public CustomerServiceTest()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customers = new List<Customer>
        {
            new Customer { Id = "1", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new Customer { Id = "2", FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
        };
        }

        [Fact]
        public void AddCustomer_ValidCustomer_CallsRepositoryAdd()
        {
            // Arrange
            var newCustomer = new Customer { Id = "3", FirstName = "Alice", LastName = "Green", Email = "alice.green@example.com" };
            var customerService = new CustomerService(_customerRepositoryMock.Object);

            // Act
            customerService.AddCustomer(newCustomer);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.AddCustomer(newCustomer), Times.Once);
        }

        [Fact]
        public void AddCustomer_MissingEmail_ThrowsArgumentException()
        {
            // Arrange
            var newCustomer = new Customer { Id = "3", FirstName = "Alice", LastName = "Green", Email = "" };
            var customerService = new CustomerService(_customerRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => customerService.AddCustomer(newCustomer));
        }

        [Fact]
        public void DeleteCustomer_ValidId_CallsRepositoryDelete()
        {
            // Arrange
            var customerId = "1";
            _customerRepositoryMock.Setup(repo => repo.GetCustomerById(customerId)).Returns(_customers.First(c => c.Id == customerId));
            var customerService = new CustomerService(_customerRepositoryMock.Object);

            // Act
            customerService.DeleteCustomer(customerId);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.DeleteCustomer(customerId), Times.Once);
        }

        [Fact]
        public void DeleteCustomer_InvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var customerId = "3";
            _customerRepositoryMock.Setup(repo => repo.GetCustomerById(customerId)).Returns((Customer?)null);
            var customerService = new CustomerService(_customerRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => customerService.DeleteCustomer(customerId));
        }

        [Fact]
        public void GetCustomers_ReturnsCustomerList()
        {
            // Arrange
            _customerRepositoryMock.Setup(repo => repo.GetCustomers()).Returns(_customers);
            var customerService = new CustomerService(_customerRepositoryMock.Object);

            // Act
            var result = customerService.GetCustomers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_customers.Count, result.Count);
        }

        [Fact]
        public void GetCustomerById_ValidId_ReturnsCustomer()
        {
            // Arrange
            var customerId = "1";
            _customerRepositoryMock.Setup(repo => repo.GetCustomerById(customerId)).Returns(_customers.First(c => c.Id == customerId));
            var customerService = new CustomerService(_customerRepositoryMock.Object);

            // Act
            var result = customerService.GetCustomerById(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public void GetCustomerById_InvalidId_ReturnsNull()
        {
            // Arrange
            var customerId = "3";
            _customerRepositoryMock.Setup(repo => repo.GetCustomerById(customerId)).Returns((Customer?)null);
            var customerService = new CustomerService(_customerRepositoryMock.Object);

            // Act
            var result = customerService.GetCustomerById(customerId);

            // Assert
            Assert.Null(result);
        }
    }
}