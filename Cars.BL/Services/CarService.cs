using Cars.BL.Interfaces;
using Cars.DL.Interfaces;
using Cars.Models.DTO;
using KafkaCacheDistributor.Cache;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Cars.BL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task AddCarAsync(Car car)
        {
            if (car.Price <= 0)
            {
                throw new ArgumentException("Car price must be greater than zero.");
            }

            await _carRepository.AddCarAsync(car);
        }

        public async Task DeleteCarAsync(string id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null)
            {
                throw new KeyNotFoundException("Car not found.");
            }

            await _carRepository.DeleteCarAsync(id);
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            return await _carRepository.GetCarsAsync();
        }

        public async Task<Car?> GetCarByIdAsync(string id)
        {
            return await _carRepository.GetCarByIdAsync(id);
        }

        
        public List<Car> GetCachedCars()
        {
            return InMemoryCarCache.GetAll();
        }
    }
}
