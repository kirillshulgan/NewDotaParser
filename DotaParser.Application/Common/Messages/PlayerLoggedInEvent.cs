namespace DotaParser.Application.Common.Messages;

/// <summary>
/// Событие, которое отправляется в шину сообщений (RabbitMQ) при успешном логине игрока.
/// </summary>
public record PlayerLoggedInEvent
{
    public long AccountId { get; init; }
    public DateTime LoggedInAt { get; init; }
}