using DotaParser.Application.Common.Models.Constants;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DotaParser.Infrastructure.Services;

public class DotaConstantsProvider : IDotaConstantsProvider
{
    private readonly IDistributedCache _cache;

    public DotaConstantsProvider(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<HeroConstant?> GetHeroByIdAsync(int heroId, CancellationToken cancellationToken)
    {
        var cacheKey = $"constant_hero_{heroId}";

        var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);

        if (string.IsNullOrEmpty(cachedData))
            return null;

        return JsonSerializer.Deserialize<HeroConstant>(cachedData);
    }
}