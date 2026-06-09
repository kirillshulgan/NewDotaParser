using DotaParser.Application.Common.Interfaces;
using DotaParser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotaParser.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<PlayerMatch> PlayerMatches => Set<PlayerMatch>();
    public DbSet<Hero> Heroes => Set<Hero>();
    public DbSet<HeroRole> HeroRoles => Set<HeroRole>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<League> Leagues => Set<League>();
    public DbSet<LeagueTeam> LeagueTeams => Set<LeagueTeam>();
    public DbSet<PlayerHero> PlayerHeroes => Set<PlayerHero>();
    public DbSet<PlayerPeer> PlayerPeers => Set<PlayerPeer>();
    public DbSet<PlayerRating> PlayerRatings => Set<PlayerRating>();
    public DbSet<PlayerRanking> PlayerRankings => Set<PlayerRanking>();
    public DbSet<PickBan> PicksBans => Set<PickBan>();
    public DbSet<TeamPlayer> TeamPlayers => Set<TeamPlayer>();
    public DbSet<TeamHero> TeamHeroes => Set<TeamHero>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<Objective> Objectives => Set<Objective>();
    public DbSet<DraftTiming> DraftTimings => Set<DraftTiming>();
    public DbSet<Teamfight> Teamfights => Set<Teamfight>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // PlayerMatch — составной PK
        modelBuilder.Entity<PlayerMatch>()
            .HasKey(pm => new { pm.MatchId, pm.AccountId });

        modelBuilder.Entity<PlayerMatch>()
            .HasOne(pm => pm.Match)
            .WithMany(m => m.PlayerMatches)
            .HasForeignKey(pm => pm.MatchId);

        modelBuilder.Entity<PlayerMatch>()
            .HasOne(pm => pm.Player)
            .WithMany(p => p.PlayerMatches)
            .HasForeignKey(pm => pm.AccountId)
            .IsRequired(false);

        modelBuilder.Entity<PlayerMatch>()
            .HasOne(pm => pm.Hero)
            .WithMany(h => h.PlayerMatches)
            .HasForeignKey(pm => pm.HeroId);

        // PlayerHero — составной PK
        modelBuilder.Entity<PlayerHero>()
            .HasKey(ph => new { ph.AccountId, ph.HeroId });

        // PlayerPeer — составной PK (self-referencing)
        modelBuilder.Entity<PlayerPeer>()
            .HasKey(pp => new { pp.AccountId, pp.PeerAccountId });

        modelBuilder.Entity<PlayerPeer>()
            .HasOne(pp => pp.Player)
            .WithMany(p => p.PeersAsPlayer)
            .HasForeignKey(pp => pp.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PlayerPeer>()
            .HasOne(pp => pp.Peer)
            .WithMany(p => p.PeersAsPeer)
            .HasForeignKey(pp => pp.PeerAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        // PlayerRanking — составной PK
        modelBuilder.Entity<PlayerRanking>()
            .HasKey(pr => new { pr.AccountId, pr.HeroId });

        // HeroRole — составной PK
        modelBuilder.Entity<HeroRole>()
            .HasKey(hr => new { hr.HeroId, hr.Role });

        // LeagueTeam — составной PK
        modelBuilder.Entity<LeagueTeam>()
            .HasKey(lt => new { lt.LeagueId, lt.TeamId });

        // TeamPlayer — составной PK
        modelBuilder.Entity<TeamPlayer>()
            .HasKey(tp => new { tp.TeamId, tp.AccountId });

        // TeamHero — составной PK
        modelBuilder.Entity<TeamHero>()
            .HasKey(th => new { th.TeamId, th.HeroId });

        // PickBan
        modelBuilder.Entity<PickBan>()
            .HasOne(pb => pb.Hero)
            .WithMany(h => h.PicksBans)
            .HasForeignKey(pb => pb.HeroId);

        // Match — два FK на Team (нужны разные имена)
        modelBuilder.Entity<Match>()
            .HasOne(m => m.RadiantTeam)
            .WithMany(t => t.RadiantMatches)
            .HasForeignKey(m => m.RadiantTeamId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.DireTeam)
            .WithMany(t => t.DireMatches)
            .HasForeignKey(m => m.DireTeamId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // Player.Team
        modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId)
            .IsRequired(false);
    }
}