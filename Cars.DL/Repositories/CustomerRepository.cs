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
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customersCollection;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(ILogger<CustomerRepository> logger, IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");
                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _customersCollection = database.GetCollection<Customer>($"{nameof(Customer)}s");
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            await _customersCollection.InsertOneAsync(customer);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _customersCollection.DeleteOneAsync(customer => customer.Id == id);
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var result = await _customersCollection.FindAsync(customer => true);
            return await result.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(string id)
        {
            var result = await _customersCollection.FindAsync(customer => customer.Id == id);
            return await result.FirstOrDefaultAsync();
        }
    }
}