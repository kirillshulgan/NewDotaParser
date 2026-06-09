namespace DotaParser.Domain.Entities;

public class Teamfight
{
    public int Id { get; set; }
    public long MatchId { get; set; }
    public int Start { get; set; }
    public int End { get; set; }
    public int LastDeath { get; set; }
    public int Deaths { get; set; }

    public Match Match { get; set; } = null!;
}