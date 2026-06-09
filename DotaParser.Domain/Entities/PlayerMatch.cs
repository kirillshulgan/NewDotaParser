namespace DotaParser.Domain.Entities;

public class PlayerMatch
{
    public long MatchId { get; set; }
    public int AccountId { get; set; }
    public int PlayerSlot { get; set; }
    public bool RadiantWin { get; set; }
    public int Duration { get; set; }
    public int GameMode { get; set; }
    public int LobbyType { get; set; }
    public int HeroId { get; set; }
    public DateTime StartTime { get; set; }
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }
    public int? Skill { get; set; }
    public int? AverageRank { get; set; }
    public int LeaverStatus { get; set; }
    public int? PartySize { get; set; }
    public int? HeroVariant { get; set; }

    // Navigation
    public Match Match { get; set; } = null!;
    public Player? Player { get; set; }
    public Hero Hero { get; set; } = null!;
}