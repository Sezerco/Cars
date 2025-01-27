using Cars.BL.Interfaces;
using Cars.DL.Interfaces;
using Cars.Models.DTO;

namespace Cars.BL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public void AddCar(Car car)
        {
            if (car.Price <= 0)
            {
                throw new ArgumentException("Car price must be greater than zero.");
            }

            _carRepository.AddCar(car);
        }

        public void DeleteCar(string id)
        {
            var car = _carRepository.GetCarById(id);
            if (car == null)
            {
                throw new KeyNotFoundException("Car not found.");
            }

            _carRepository.DeleteCar(id);
        }

        public List<Car> GetCars()
        {
            return _carRepository.GetCars();
        }

        public Car? GetCarById(string id)
        {
            return _carRepository.GetCarById(id);
        }

    }
}
