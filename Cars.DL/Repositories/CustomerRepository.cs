using Cars.DL.Interfaces;
using Cars.Models.Configurations;
using Cars.Models.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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

        public void AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            _customersCollection.InsertOne(customer);
        }

        public void DeleteCustomer(string id)
        {
            _customersCollection.DeleteOne(customer => customer.Id == id);
        }

        public List<Customer> GetCustomers()
        {
            return _customersCollection.Find(customer => true).ToList();
        }

        public Customer? GetCustomerById(string id)
        {
            return _customersCollection.Find(customer => customer.Id == id).FirstOrDefault();
        }
    }
}
