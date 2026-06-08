namespace DotaParser.Application.Common.Caching;

public interface ICacheableQuery
{
    // Ключ, по которому будем сохранять/искать данные в Redis
    string CacheKey { get; }

    // Время жизни кэша (чтобы для разных запросов можно было задавать разное время)
    TimeSpan? AbsoluteExpirationRelativeToNow { get; }
}