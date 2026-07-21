namespace EmployeeManagementAPI.DTOs;

public class ChatMessageDto
{
    public string Sender { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime SentAtUtc { get; set; } = DateTime.UtcNow;
}
