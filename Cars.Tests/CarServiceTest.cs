using Cars.BL.Services;
using Cars.DL.Interfaces;
using Cars.Models.DTO;
using Moq;

namespace Cars.Tests
{
    public class CarServiceTest
    {
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly List<Car> _cars;

        public CarServiceTest()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _cars = new List<Car>
            {
                new Car { Id = "1", CustomerId = "100", Model = "Toyota", Year = 2020, Price = 25000m },
                new Car { Id = "2", CustomerId = "101", Model = "Honda", Year = 2021, Price = 30000m }
            };
        }

        [Fact]
        public void AddCar_ValidCar_CallsRepositoryAdd()
        {
            // Arrange
            var newCar = new Car { Id = "3", CustomerId = "102", Model = "BMW", Year = 2022, Price = 40000m };
            var carService = new CarService(_carRepositoryMock.Object);

            // Act
            carService.AddCar(newCar);

            // Assert
            _carRepositoryMock.Verify(repo => repo.AddCar(newCar), Times.Once);
        }

        [Fact]
        public void AddCar_InvalidPrice_ThrowsArgumentException()
        {
            // Arrange
            var newCar = new Car { Id = "3", CustomerId = "102", Model = "BMW", Year = 2022, Price = -1000m };
            var carService = new CarService(_carRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => carService.AddCar(newCar));
        }

        [Fact]
        public void DeleteCar_ValidId_CallsRepositoryDelete()
        {
            // Arrange
            var carId = "1";
            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).Returns(_cars.First(c => c.Id == carId));
            var carService = new CarService(_carRepositoryMock.Object);

            // Act
            carService.DeleteCar(carId);

            // Assert
            _carRepositoryMock.Verify(repo => repo.DeleteCar(carId), Times.Once);
        }

        [Fact]
        public void DeleteCar_InvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var carId = "3";
            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).Returns((Car?)null);
            var carService = new CarService(_carRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => carService.DeleteCar(carId));
        }

        [Fact]
        public void GetCars_ReturnsCarList()
        {
            // Arrange
            _carRepositoryMock.Setup(repo => repo.GetCars()).Returns(_cars);
            var carService = new CarService(_carRepositoryMock.Object);

            // Act
            var result = carService.GetCars();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_cars.Count, result.Count);
        }

        [Fact]
        public void GetCarById_ValidId_ReturnsCar()
        {
            // Arrange
            var carId = "1";
            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).Returns(_cars.First(c => c.Id == carId));
            var carService = new CarService(_carRepositoryMock.Object);

            // Act
            var result = carService.GetCarById(carId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(carId, result.Id);
        }

        [Fact]
        public void GetCarById_InvalidId_ReturnsNull()
        {
            // Arrange
            var carId = "3";
            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).Returns((Car?)null);
            var carService = new CarService(_carRepositoryMock.Object);

            // Act
            var result = carService.GetCarById(carId);

            // Assert
            Assert.Null(result);
        }
    }
}
