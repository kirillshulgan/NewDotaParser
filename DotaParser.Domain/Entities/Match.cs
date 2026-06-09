namespace DotaParser.Domain.Entities;

public class Match
{
    public Guid Id { get; set; }
    public long MatchId { get; set; }           // PK
    public int BarrackStatusDire { get; set; }
    public int BarrackStatusRadiant { get; set; }
    public int Cluster { get; set; }
    public int DireScore { get; set; }
    public int Duration { get; set; }
    public int Engine { get; set; }
    public int FirstBloodTime { get; set; }
    public int GameMode { get; set; }
    public int HumanPlayers { get; set; }
    public int LobbyType { get; set; }
    public long MatchSeqNum { get; set; }
    public int NegativeVotes { get; set; }
    public int PositiveVotes { get; set; }
    public int RadiantScore { get; set; }
    public bool RadiantWin { get; set; }
    public DateTime StartTime { get; set; }
    public int TowerStatusDire { get; set; }
    public int TowerStatusRadiant { get; set; }
    public int Version { get; set; }
    public long ReplaySalt { get; set; }
    public int SeriesId { get; set; }
    public int SeriesType { get; set; }
    public int? Skill { get; set; }
    public int Patch { get; set; }
    public int Region { get; set; }
    public int? Throw { get; set; }
    public int? Comeback { get; set; }
    public int? Loss { get; set; }
    public int? Win { get; set; }
    public string? ReplayUrl { get; set; }

    // FK
    public int? LeagueId { get; set; }
    public int? RadiantTeamId { get; set; }
    public int? DireTeamId { get; set; }

    // Navigation
    public League? League { get; set; }
    public Team? RadiantTeam { get; set; }
    public Team? DireTeam { get; set; }
    public ICollection<PlayerMatch> PlayerMatches { get; set; } = [];
    public ICollection<PickBan> PicksBans { get; set; } = [];
    public ICollection<ChatMessage> ChatMessages { get; set; } = [];
    public ICollection<Objective> Objectives { get; set; } = [];
    public ICollection<Teamfight> Teamfights { get; set; } = [];
    public ICollection<DraftTiming> DraftTimings { get; set; } = [];
}