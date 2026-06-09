namespace DotaParser.Domain.Entities;

public class PlayerRating
{
    public int Id { get; set; }                 // суррогатный PK
    public int AccountId { get; set; }
    public long MatchId { get; set; }
    public int RankTier { get; set; }
    public DateTime Time { get; set; }

    public Player Player { get; set; } = null!;
}