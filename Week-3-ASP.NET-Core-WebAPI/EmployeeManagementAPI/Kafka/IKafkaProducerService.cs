using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Kafka;

public interface IKafkaProducerService
{
    Task<bool> PublishAsync(ChatMessageDto message, CancellationToken cancellationToken = default);
}
