namespace DotaParser.Domain.Entities;

public class Objective
{
    public int Id { get; set; }
    public long MatchId { get; set; }
    public int Time { get; set; }
    public string? Type { get; set; }
    public int? PlayerSlot { get; set; }
    public int? Team { get; set; }
    public string? Key { get; set; }

    public Match Match { get; set; } = null!;
}