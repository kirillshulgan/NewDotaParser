using DotaParser.Application.Common.DTOs;

namespace DotaParser.Application.Common.Interfaces;

public interface IOpenDotaService
{
    // CancellationToken обязателен для асинхронных вызовов во внешние API
    Task<PlayerProfileDto?> GetPlayerProfileAsync(long accountId, CancellationToken cancellationToken);

    Task<List<MatchDto>> GetPlayerMatchesAsync(long accountId, int limit, CancellationToken cancellationToken);
}