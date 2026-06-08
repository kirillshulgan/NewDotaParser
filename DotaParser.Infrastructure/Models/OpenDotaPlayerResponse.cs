using System.Text.Json.Serialization;

namespace DotaParser.Infrastructure.Models;

public class OpenDotaPlayerResponse
{
    [JsonPropertyName("rank_tier")] 
    public int? RankTier { get; set; }

    [JsonPropertyName("leaderboard_rank")] 
    public int? LeaderboardRank { get; set; }

    [JsonPropertyName("computed_mmr")] 
    public int? ComputedMmr { get; set; }

    [JsonPropertyName("computed_mmr_turbo")] 
    public int? ComputedMmrTurbo { get; set; }

    [JsonPropertyName("profile")] 
    public OpenDotaProfile Profile { get; set; } = null!;
}