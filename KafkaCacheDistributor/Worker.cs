using Confluent.Kafka;
using KafkaCacheDistributor.Configurations;
using KafkaCacheDistributor.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMongoCollection<Car> _carsCollection;
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic;
    private readonly int _intervalSeconds;

    public Worker(
        ILogger<Worker> logger,
        IOptions<KafkaSettings> kafkaSettings,
        IOptions<MongoDbConfiguration> mongoSettings,
        IOptions<WorkerSettings> workerSettings)
    {
        _logger = logger;

        var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(mongoSettings.Value.Database);
        _carsCollection = database.GetCollection<Car>(mongoSettings.Value.Collection);

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers
        };

        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        _topic = kafkaSettings.Value.Topic;
        _intervalSeconds = workerSettings.Value.IntervalSeconds;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var cars = await _carsCollection.Find(_ => true).ToListAsync(stoppingToken);

            foreach (var car in cars)
            {
                var json = JsonSerializer.Serialize(car);
                await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = json }, stoppingToken);
            }

            _logger.LogInformation($"Published {cars.Count} cars to Kafka.");
            await Task.Delay(TimeSpan.FromSeconds(_intervalSeconds), stoppingToken);
        }
    }
}