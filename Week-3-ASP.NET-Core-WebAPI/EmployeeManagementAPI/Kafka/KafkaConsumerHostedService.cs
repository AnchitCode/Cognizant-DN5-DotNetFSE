using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace EmployeeManagementAPI.Kafka;

public class KafkaConsumerHostedService : BackgroundService
{
    private readonly ILogger<KafkaConsumerHostedService> logger;
    private readonly KafkaOptions kafkaOptions;

    public KafkaConsumerHostedService(ILogger<KafkaConsumerHostedService> logger, IOptions<KafkaOptions> kafkaOptions)
    {
        this.logger = logger;
        this.kafkaOptions = kafkaOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = kafkaOptions.BootstrapServers,
                    GroupId = kafkaOptions.GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = true,
                    AllowAutoCreateTopics = true
                };

                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(kafkaOptions.Topic);
                logger.LogInformation("Kafka consumer started for topic {Topic}", kafkaOptions.Topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(stoppingToken);
                        if (result?.Message.Value is null)
                        {
                            continue;
                        }

                        logger.LogInformation("Kafka message received: {Message}", result.Message.Value);
                        Console.WriteLine(result.Message.Value);
                    }
                    catch (ConsumeException consumeException)
                    {
                        logger.LogWarning(consumeException, "Kafka consume error");
                    }
                }
            }
            catch (Exception exception)
            {
                logger.LogWarning(exception, "Kafka consumer unavailable, retrying shortly");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
