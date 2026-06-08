using DotaParser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotaParser.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Player> Players { get; }
    DbSet<Match> Matches { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}