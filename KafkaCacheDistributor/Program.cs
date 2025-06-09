using KafkaCacheDistributor;
using KafkaCacheDistributor.Configurations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<KafkaSettings>(context.Configuration.GetSection("Kafka"));
        services.Configure<MongoDbConfiguration>(context.Configuration.GetSection("MongoDb"));
        services.Configure<WorkerSettings>(context.Configuration.GetSection("WorkerSettings"));

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();