namespace DotaParser.Domain.Entities;

public class PlayerHero
{
    public int AccountId { get; set; }
    public int HeroId { get; set; }
    public DateTime? LastPlayed { get; set; }
    public int Games { get; set; }
    public int Win { get; set; }
    public int WithGames { get; set; }
    public int WithWin { get; set; }
    public int AgainstGames { get; set; }
    public int AgainstWin { get; set; }

    public Player Player { get; set; } = null!;
    public Hero Hero { get; set; } = null!;
}