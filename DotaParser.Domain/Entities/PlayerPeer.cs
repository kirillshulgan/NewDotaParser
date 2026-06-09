namespace DotaParser.Domain.Entities;

public class PlayerPeer
{
    public int AccountId { get; set; }
    public int PeerAccountId { get; set; }
    public DateTime? LastPlayed { get; set; }
    public int Win { get; set; }
    public int Games { get; set; }
    public int WithWin { get; set; }
    public int WithGames { get; set; }
    public int AgainstWin { get; set; }
    public int AgainstGames { get; set; }
    public int? WithGpmSum { get; set; }
    public int? WithXpmSum { get; set; }

    public Player Player { get; set; } = null!;
    public Player Peer { get; set; } = null!;
}