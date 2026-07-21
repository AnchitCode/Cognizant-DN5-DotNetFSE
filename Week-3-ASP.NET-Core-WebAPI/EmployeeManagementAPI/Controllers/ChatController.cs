using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IKafkaProducerService kafkaProducerService;

    public ChatController(IKafkaProducerService kafkaProducerService)
    {
        this.kafkaProducerService = kafkaProducerService;
    }

    [HttpPost("publish")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> PublishAsync([FromBody] ChatMessageDto message, CancellationToken cancellationToken)
    {
        var success = await kafkaProducerService.PublishAsync(message, cancellationToken);
        if (!success)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "Kafka broker is unavailable");
        }

        return Accepted(message);
    }
}
