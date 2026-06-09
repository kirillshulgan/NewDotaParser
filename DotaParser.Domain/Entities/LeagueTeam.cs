namespace DotaParser.Domain.Entities;

public class LeagueTeam
{
    public int LeagueId { get; set; }
    public int TeamId { get; set; }

    public League League { get; set; } = null!;
    public Team Team { get; set; } = null!;
}