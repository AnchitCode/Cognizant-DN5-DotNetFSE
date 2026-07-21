using System.Text.Json;
using EmployeeManagementAPI.DTOs;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace EmployeeManagementAPI.Kafka;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly KafkaOptions kafkaOptions;

    public KafkaProducerService(IOptions<KafkaOptions> kafkaOptions)
    {
        this.kafkaOptions = kafkaOptions.Value;
    }

    public async Task<bool> PublishAsync(ChatMessageDto message, CancellationToken cancellationToken = default)
    {
        try
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaOptions.BootstrapServers,
                Acks = Acks.All
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var payload = JsonSerializer.Serialize(message);
            await producer.ProduceAsync(kafkaOptions.Topic, new Message<Null, string> { Value = payload }, cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
