namespace DotaParser.Domain.Entities;

/// <summary> Игрок </summary>
public class Player
{
    /// <summary> Id в нашей БД </summary>
    public Guid Id { get; set; }

    public long AccountId { get; set; }          // Steam32 account ID (PK)
    public string? PersonaName { get; set; }
    public string? Name { get; set; }
    public bool Plus { get; set; }
    public int Cheese { get; set; }
    public string? SteamId { get; set; }
    public string? Avatar { get; set; }
    public string? AvatarMedium { get; set; }
    public string? AvatarFull { get; set; }
    public string? ProfileUrl { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? LocCountryCode { get; set; }
    public bool IsContributor { get; set; }
    public bool IsSubscriber { get; set; }

    // Rank/MMR
    public int? RankTier { get; set; }
    public int? LeaderboardRank { get; set; }
    public int? ComputedMmr { get; set; }
    public int? ComputedMmrTurbo { get; set; }

    // Pro player fields
    public string? CountryCode { get; set; }
    public int? FantasyRole { get; set; }
    public int? LockedUntil { get; set; }
    public bool IsLocked { get; set; }
    public bool IsPro { get; set; }
    public DateTime? FullHistoryTime { get; set; }
    public bool FhUnavailable { get; set; }

    // Navigation
    public int? TeamId { get; set; }
    public Team? Team { get; set; }

    public ICollection<PlayerMatch> PlayerMatches { get; set; } = [];
    public ICollection<PlayerHero> PlayerHeroes { get; set; } = [];
    public ICollection<PlayerRating> Ratings { get; set; } = [];
    public ICollection<PlayerRanking> Rankings { get; set; } = [];
    public ICollection<PlayerPeer> PeersAsPlayer { get; set; } = [];
    public ICollection<PlayerPeer> PeersAsPeer { get; set; } = [];
}