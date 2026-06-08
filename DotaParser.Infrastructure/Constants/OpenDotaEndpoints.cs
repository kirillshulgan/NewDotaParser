namespace DotaParser.Infrastructure.Constants;

/// <summary>
/// Содержит все маршруты к OpenDota API.
/// Базовый URL: https://api.opendota.com/api/
/// </summary>
public static class OpenDotaEndpoints
{
    // ==========================================
    // ИГРОКИ (PLAYERS)
    // ==========================================

    /// <summary>
    /// Данные профиля игрока.
    /// GET /players/{account_id}
    /// </summary>
    public static string PlayerProfile(long accountId) => $"players/{accountId}";

    /// <summary>
    /// История сыгранных матчей игрока.
    /// GET /players/{account_id}/matches
    /// Можно передавать параметры запроса: limit, offset, win, patch, game_mode, hero_id и т.д.
    /// </summary>
    public static string PlayerMatches(long accountId, int limit = 20) => $"players/{accountId}/matches?limit={limit}";

    /// <summary>
    /// Количество побед и поражений игрока.
    /// GET /players/{account_id}/wl
    /// </summary>
    public static string PlayerWinLoss(long accountId) => $"players/{accountId}/wl";

    /// <summary>
    /// Недавние матчи игрока (обычно последние 20).
    /// GET /players/{account_id}/recentMatches
    /// </summary>
    public static string PlayerRecentMatches(long accountId) => $"players/{accountId}/recentMatches";

    /// <summary>
    /// Статистика игрока по героям.
    /// GET /players/{account_id}/heroes
    /// </summary>
    public static string PlayerHeroes(long accountId) => $"players/{accountId}/heroes";

    /// <summary>
    /// Принудительно обновляет историю матчей игрока из реплеев Dota 2 (Rate-limited!).
    /// POST /players/{account_id}/refresh
    /// </summary>
    public static string PlayerRefresh(long accountId) => $"players/{accountId}/refresh";


    // ==========================================
    // МАТЧИ (MATCHES)
    // ==========================================

    /// <summary>
    /// Детальная информация по конкретному матчу.
    /// GET /matches/{match_id}
    /// </summary>
    public static string MatchDetails(long matchId) => $"matches/{matchId}";

    /// <summary>
    /// Список профессиональных матчей.
    /// GET /proMatches
    /// </summary>
    public const string ProMatches = "proMatches";

    /// <summary>
    /// Список публичных матчей.
    /// GET /publicMatches
    /// </summary>
    public const string PublicMatches = "publicMatches";


    // ==========================================
    // ГЕРОИ И МЕТА (HEROES & META)
    // ==========================================

    /// <summary>
    /// Базовый список всех героев.
    /// GET /heroes
    /// </summary>
    public const string Heroes = "heroes";

    /// <summary>
    /// Расширенная статистика героев (винрейты по рангам, пикрейты).
    /// GET /heroStats
    /// </summary>
    public const string HeroStats = "heroStats";


    // ==========================================
    // ПОИСК (SEARCH)
    // ==========================================

    /// <summary>
    /// Поиск игроков по имени.
    /// GET /search?q={personaname}
    /// </summary>
    public static string SearchPlayers(string personaName) => $"search?q={personaName}";
}