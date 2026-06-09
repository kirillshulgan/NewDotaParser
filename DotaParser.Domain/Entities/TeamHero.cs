namespace DotaParser.Domain.Entities;

public class TeamHero
{
    public int TeamId { get; set; }
    public int HeroId { get; set; }
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }

    public Team Team { get; set; } = null!;
    public Hero Hero { get; set; } = null!;
}