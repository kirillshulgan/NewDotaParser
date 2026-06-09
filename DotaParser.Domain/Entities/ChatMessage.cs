namespace DotaParser.Domain.Entities;

public class ChatMessage
{
    public int Id { get; set; }
    public long MatchId { get; set; }
    public int? PlayerSlot { get; set; }
    public int Time { get; set; }
    public string? Key { get; set; }
    public string? Type { get; set; }

    public Match Match { get; set; } = null!;
}