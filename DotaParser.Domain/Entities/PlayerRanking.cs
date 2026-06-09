namespace DotaParser.Domain.Entities;

public class PlayerRanking
{
    public int AccountId { get; set; }
    public int HeroId { get; set; }
    public float Score { get; set; }
    public float PercentRank { get; set; }
    public int? Card { get; set; }

    public Player Player { get; set; } = null!;
    public Hero Hero { get; set; } = null!;
}