using DotaParser.Application.Common.Caching;
using DotaParser.Application.Common.DTOs;
using MediatR;

namespace DotaParser.Application.Players.Queries.GetPlayerProfile;

public record GetPlayerProfileQuery(long AccountId) : IRequest<PlayerProfileDto?>, ICacheableQuery
{
    // Формируем уникальный ключ для Redis (например: player_profile_34505203)
    public string CacheKey => $"player_profile_{AccountId}";

    // Кэшируем профиль на 1 час
    public TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromHours(1);
}