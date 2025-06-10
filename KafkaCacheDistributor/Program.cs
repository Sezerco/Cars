using KafkaCacheDistributor.Configurations;
using KafkaCacheDistributor.Consumers;
using KafkaCacheDistributor.Services;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<KafkaSettings>(context.Configuration.GetSection("Kafka"));
        services.Configure<MongoDbConfiguration>(context.Configuration.GetSection("MongoDb"));
        services.Configure<WorkerSettings>(context.Configuration.GetSection("WorkerSettings"));

        services.AddHostedService<Worker>();              
        services.AddHostedService<KafkaConsumerWorker>();  
    })
    .Build();

await host.RunAsync();