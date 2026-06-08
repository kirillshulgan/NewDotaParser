namespace DotaParser.Domain.Entities;

/// <summary> Игрок </summary>
public class Player
{
    /// <summary> Id в нашей БД </summary>
    public Guid Id { get; set; }

    /// <summary> Steam Account ID (он же используется в OpenDota) </summary>
    public long SteamAccountId { get; set; }

    /// <summary> Имя пользователя </summary>
    public string PersonaName { get; set; } = string.Empty;

    /// <summary> Ссылка на аватар </summary>
    public string AvatarUrl { get; set; } = string.Empty;

    /// <summary> Ссылка на профиль в Steam </summary>
    public string ProfileUrl { get; set; } = string.Empty;

    public int? RankTier { get; set; }
    public int? LeaderboardRank { get; set; }
    public string? CountryCode { get; set; }
    public bool IsDotaPlus { get; set; }

    public ICollection<Match> Matches { get; set; } = new List<Match>();
}