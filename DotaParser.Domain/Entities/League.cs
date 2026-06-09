namespace DotaParser.Domain.Entities;

public class League
{
    public int LeagueId { get; set; }           // PK
    public string? Ticket { get; set; }
    public string? Banner { get; set; }
    public string? Tier { get; set; }
    public string? Name { get; set; }           // "ASUS ROG DreamLeague Season 4"

    // Navigation
    public ICollection<Match> Matches { get; set; } = [];
    public ICollection<LeagueTeam> LeagueTeams { get; set; } = [];
}