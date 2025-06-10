
using Confluent.Kafka;
using KafkaCacheDistributor.Cache;
using KafkaCacheDistributor.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace KafkaCacheDistributor.Consumers
{
    public class KafkaConsumerWorker : BackgroundService
    {
        private readonly ILogger<KafkaConsumerWorker> _logger;
        private readonly string _bootstrapServers = "127.0.0.1:9092";
        private readonly string _topic = "cars-topic";

        public KafkaConsumerWorker(ILogger<KafkaConsumerWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "car-cache-consumer",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topic);

            _logger.LogInformation("Kafka Consumer started and subscribed to topic: {Topic}", _topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(stoppingToken);
                    var car = JsonSerializer.Deserialize<Car>(result.Message.Value);

                    if (car != null)
                    {
                        InMemoryCarCache.AddOrUpdate(car);
                        _logger.LogInformation($"[KafkaConsumer] Cached car: {car.Id} - {car.Make} {car.Model}");
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Kafka consume error");
                }

                await Task.Delay(100, stoppingToken);
            }
        }
    }
}