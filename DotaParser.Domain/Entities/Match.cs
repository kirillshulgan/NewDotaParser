namespace DotaParser.Domain.Entities;

public class Match
{
    public Guid Id { get; set; }
    public long DotaMatchId { get; set; }
    public long PlayerAccountId { get; set; }

    public int PlayerSlot { get; set; }
    public bool RadiantWin { get; set; }

    /// <summary>
    /// Продолжительность в секундах
    /// </summary>
    public int Duration { get; set; }

    public int GameMode { get; set; }
    public int LobbyType { get; set; }
    public int HeroId { get; set; }

    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }

    public int? AverageRank { get; set; }
    public int LeaverStatus { get; set; }
    public int? PartySize { get; set; }

    public DateTime StartTime { get; set; }

    public Player Player { get; set; } = null!;
}