using Cars.DL.Interfaces;
using Cars.Models.Configurations;
using Cars.Models.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.DL.Repositories
{
    internal class CarRepository : ICarRepository
    {
        private readonly IMongoCollection<Car> _carsCollection;
        private readonly ILogger<CarRepository> _logger;

        public CarRepository(ILogger<CarRepository> logger, IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");
                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _carsCollection = database.GetCollection<Car>($"{nameof(Car)}s");
        }

        public async Task AddCarAsync(Car car)
        {
            car.Id = Guid.NewGuid().ToString();
            await _carsCollection.InsertOneAsync(car);
        }

        public async Task DeleteCarAsync(string id)
        {
            await _carsCollection.DeleteOneAsync(car => car.Id == id);
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            var result = await _carsCollection.FindAsync(car => true);
            return await result.ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(string id)
        {
            var result = await _carsCollection.FindAsync(car => car.Id == id);
            return await result.FirstOrDefaultAsync();
        }
    }
}