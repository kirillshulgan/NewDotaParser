namespace DotaParser.Domain.Entities;

public class DraftTiming
{
    public int Id { get; set; }
    public long MatchId { get; set; }
    public int Order { get; set; }
    public int Pick { get; set; }
    public int ActiveTeam { get; set; }
    public int HeroId { get; set; }
    public int? PlayerSlot { get; set; }
    public float? ExtraTime { get; set; }
    public float? TotalTimeTaken { get; set; }

    public Match Match { get; set; } = null!;
}