using DotaParser.Application.Common.Interfaces;
using DotaParser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotaParser.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>
    /// Таблица игроков, авторизованных в нашей системе.
    /// </summary>
    public DbSet<Player> Players => Set<Player>();

    /// <summary>
    /// Таблица игроков, авторизованных в нашей системе.
    /// </summary>
    public DbSet<Match> Matches => Set<Match>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Настраиваем уникальность AccountId
        builder.Entity<Player>()
            .HasIndex(p => p.SteamAccountId)
            .IsUnique();

        builder.Entity<Match>()
            .HasIndex(m => m.DotaMatchId);

        builder.Entity<Match>()
            .HasOne(m => m.Player)
            .WithMany(p => p.Matches)
            .HasPrincipalKey(p => p.SteamAccountId) // Связываем по SteamAccountId
            .HasForeignKey(m => m.PlayerAccountId); // с полем PlayerAccountId в таблице Match
    }
}