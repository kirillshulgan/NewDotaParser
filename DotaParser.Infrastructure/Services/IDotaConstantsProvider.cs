using DotaParser.Application.Common.Models.Constants;

namespace DotaParser.Infrastructure.Services;

public interface IDotaConstantsProvider
{
    Task<HeroConstant?> GetHeroByIdAsync(int heroId, CancellationToken cancellationToken);
}