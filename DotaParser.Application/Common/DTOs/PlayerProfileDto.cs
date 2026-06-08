namespace DotaParser.Application.Common.DTOs;

public class PlayerProfileDto
{
    public long AccountId { get; set; }
    public string PersonaName { get; set; } = string.Empty;
    public string AvatarFull { get; set; } = string.Empty;
    public string? LocCountryCode { get; set; }
    public bool IsDotaPlusSubscriber { get; set; }
    public int? RankTier { get; set; }
    public int? LeaderboardRank { get; set; }
    public int? ComputedMmr { get; set; }
}