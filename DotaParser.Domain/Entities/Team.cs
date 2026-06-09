namespace DotaParser.Domain.Entities;

public class Team
{
    public int TeamId { get; set; }             // PK
    public float Rating { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public DateTime? LastMatchTime { get; set; }
    public string? Name { get; set; }           // "Newbee"
    public string? Tag { get; set; }

    // Navigation
    public ICollection<Player> Players { get; set; } = [];
    public ICollection<Match> RadiantMatches { get; set; } = [];
    public ICollection<Match> DireMatches { get; set; } = [];
    public ICollection<TeamPlayer> TeamPlayers { get; set; } = [];
    public ICollection<TeamHero> TeamHeroes { get; set; } = [];
    public ICollection<LeagueTeam> LeagueTeams { get; set; } = [];
}