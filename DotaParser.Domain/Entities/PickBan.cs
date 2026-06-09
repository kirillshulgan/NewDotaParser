namespace DotaParser.Domain.Entities;

public class PickBan
{
    public int Id { get; set; }
    public long MatchId { get; set; }
    public bool IsPick { get; set; }
    public int HeroId { get; set; }
    public int Team { get; set; }
    public int Order { get; set; }

    public Match Match { get; set; } = null!;
    public Hero Hero { get; set; } = null!;
}