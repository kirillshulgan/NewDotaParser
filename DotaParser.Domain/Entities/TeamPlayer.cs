namespace DotaParser.Domain.Entities;

public class TeamPlayer
{
    public int TeamId { get; set; }
    public int AccountId { get; set; }
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }
    public bool IsCurrentTeamMember { get; set; }

    public Team Team { get; set; } = null!;
    public Player? Player { get; set; }
}