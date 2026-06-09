namespace DotaParser.Infrastructure.Constants;

/// <summary>
/// Статические маршруты OpenDota API (31.1.0).
/// Базовый URL: https://api.opendota.com/api
/// </summary>
public static class OpenDotaEndpoints
{
    /// <summary> Данные матча </summary>
    public static class Matches
    {
        /// <summary> Данные матча по ID. GET /matches/{match_id} </summary>
        public static string GetMatch(long matchId) => $"matches/{matchId}";
    }
    
    /// <summary> Данные игрока </summary>
    public static class Players
    {
        /// <summary> Данные профиля игрока (ранг, MMR, Steam-профиль). GET /players/{account_id} </summary>
        public static string GetPlayerProfile(long accountId) => $"players/{accountId}";

        /// <summary> Количество побед и поражений игрока. GET /players/{account_id}/wl </summary>
        public static string GetPlayerWinLoss(long accountId) => $"players/{accountId}/wl";

        /// <summary> Последние сыгранные матчи (ограниченное число). GET /players/{account_id}/recentMatches </summary>
        public static string GetPlayerRecentMatches(long accountId) => $"players/{accountId}/recentMatches";

        /// <summary> Полная история матчей игрока с поддержкой фильтрации и выбора полей. GET /players/{account_id}/matches </summary>
        public static string GetPlayerMatches(long accountId, int? limit = null, int? offset = null, int? heroId = null, int? gameMode = null, int? lobbyType = null) =>
            $"players/{accountId}/matches" + BuildQuery(
                ("limit",     limit?.ToString()),
                ("offset",    offset?.ToString()),
                ("hero_id",   heroId?.ToString()),
                ("game_mode", gameMode?.ToString()),
                ("lobby_type",lobbyType?.ToString())
            );

        /// <summary> Герои, на которых играл игрок, и их статистика. GET /players/{account_id}/heroes </summary>
        public static string GetPlayerHeroes(long accountId, int? limit = null, int? offset = null, int? heroId = null) =>
            $"players/{accountId}/heroes" + BuildQuery(
                ("limit",   limit?.ToString()),
                ("offset",  offset?.ToString()),
                ("hero_id", heroId?.ToString())
            );

        /// <summary> Игроки, с которыми играл данный игрок. GET /players/{account_id}/peers </summary>
        public static string GetPlayerPeers(long accountId, int? limit = null, int? offset = null) =>
            $"players/{accountId}/peers" + BuildQuery(
                ("limit",  limit?.ToString()),
                ("offset", offset?.ToString())
            );

        /// <summary> Про-игроки, с которыми играл данный игрок. GET /players/{account_id}/pros </summary>
        public static string GetPlayerPros(long accountId, int? limit = null, int? offset = null) =>
            $"players/{accountId}/pros" + BuildQuery(
                ("limit",  limit?.ToString()),
                ("offset", offset?.ToString())
            );

        /// <summary> Суммарная статистика по различным полям. GET /players/{account_id}/totals </summary>
        public static string GetPlayerTotals(long accountId, int? limit = null, int? heroId = null, int? gameMode = null) =>
            $"players/{accountId}/totals" + BuildQuery(
                ("limit",    limit?.ToString()),
                ("hero_id",  heroId?.ToString()),
                ("game_mode",gameMode?.ToString())
            );

        /// <summary> Количество матчей по категориям (game_mode, lobby_type, lane_role и др.). GET /players/{account_id}/counts </summary>
        public static string GetPlayerCounts(long accountId, int? heroId = null, int? gameMode = null) =>
            $"players/{accountId}/counts" + BuildQuery(
                ("hero_id",  heroId?.ToString()),
                ("game_mode",gameMode?.ToString())
            );

        /// <summary> Распределение матчей по значению одной статистики. GET /players/{account_id}/histograms/{field} </summary>
        public static string GetPlayerHistograms(long accountId, string field, int? limit = null) =>
            $"players/{accountId}/histograms/{field}" + BuildQuery(
                ("limit", limit?.ToString())
            );

        /// <summary> Карта расстановки вардов в сыгранных матчах. GET /players/{account_id}/wardmap </summary>
        public static string GetPlayerWardMap(long accountId, int? limit = null, int? heroId = null) =>
            $"players/{accountId}/wardmap" + BuildQuery(
                ("limit",   limit?.ToString()),
                ("hero_id", heroId?.ToString())
            );

        /// <summary> Слова, сказанные/прочитанные игроком в матчах. GET /players/{account_id}/wordcloud </summary>
        public static string GetPlayerWordCloud(long accountId, int? limit = null, int? heroId = null) =>
            $"players/{accountId}/wordcloud" + BuildQuery(
                ("limit",   limit?.ToString()),
                ("hero_id", heroId?.ToString())
            );

        /// <summary> История изменений ранга (медали) игрока. GET /players/{account_id}/ratings </summary>
        public static string GetPlayerRatings(long accountId) => $"players/{accountId}/ratings";

        /// <summary> Рейтинг игрока по отдельным героям. GET /players/{account_id}/rankings </summary>
        public static string GetPlayerRankings(long accountId) => $"players/{accountId}/rankings";

        /// <summary> Обновить историю матчей, медаль и имя профиля (до 500 матчей). POST /players/{account_id}/refresh </summary>
        public static string PostPlayerRefresh(long accountId) => $"players/{accountId}/refresh";
    }
    
    /// <summary> Игроки с наивысшим рейтингом </summary>
    public static class TopPlayers
    {
        /// <summary> Список игроков с наивысшим рейтингом. GET /topPlayers </summary>
        public static string GetTopPlayers(int? turbo = null) =>
            "topPlayers" + BuildQuery(("turbo", turbo?.ToString()));
    }
    
    /// <summary> Список профессиональных игроков </summary>
    public static class ProPlayers
    {
        /// <summary> Список профессиональных игроков. GET /proPlayers </summary>
        public static string GetProPlayers() => "proPlayers";
    }
    
    /// <summary> Список профессиональных матчей </summary>
    public static class ProMatches
    {
        /// <summary> Список профессиональных матчей. GET /proMatches </summary>
        public static string GetProMatches(long? lessThanMatchId = null) =>
            "proMatches" + BuildQuery(("less_than_match_id", lessThanMatchId?.ToString()));
    }
    
    /// <summary> Список случайно выбранных публичных матчей </summary>
    public static class PublicMatches
    {
        /// <summary> Список случайно выбранных публичных матчей. GET /publicMatches </summary>
        public static string GetPublicMatches(long? lessThanMatchId = null, int? minRank = null, int? maxRank = null) =>
            "publicMatches" + BuildQuery(
                ("less_than_match_id", lessThanMatchId?.ToString()),
                ("min_rank",           minRank?.ToString()),
                ("max_rank",           maxRank?.ToString())
            );
    }
    
    /// <summary> Список ID разобранных (parsed) матчей </summary>
    public static class ParsedMatches
    {
        /// <summary> Список ID разобранных (parsed) матчей. GET /parsedMatches </summary>
        public static string GetParsedMatches(long? lessThanMatchId = null) =>
            "parsedMatches" + BuildQuery(("less_than_match_id", lessThanMatchId?.ToString()));
    }
    
    /// <summary> Выполнить произвольный SQL-запрос к базе данных OpenDota </summary>
    public static class Explorer
    {
        /// <summary> Выполнить произвольный SQL-запрос к базе данных OpenDota. GET /explorer </summary>
        public static string GetExplorer(string sql) =>
            "explorer" + BuildQuery(("sql", Uri.EscapeDataString(sql)));
    }
    
    /// <summary> Метаданные сайта </summary>
    public static class Metadata
    {
        /// <summary> Метаданные сайта (баннеры и прочее). GET /metadata </summary>
        public static string GetMetadata() => "metadata";
    }
    
    /// <summary> Распределение MMR по скобкам и странам </summary>
    public static class Distributions
    {
        /// <summary> Распределение MMR по скобкам и странам. GET /distributions </summary>
        public static string GetDistributions() => "distributions";
    }
    
    /// <summary> Поиск игроков по никнейму (personaname) </summary>
    public static class Search
    {
        /// <summary> Поиск игроков по никнейму (personaname). GET /search </summary>
        public static string GetSearch(string query) =>
            "search" + BuildQuery(("q", Uri.EscapeDataString(query)));
    }
    
    /// <summary> Топ игроков по конкретному герою </summary>
    public static class Rankings
    {
        /// <summary> Топ игроков по конкретному герою. GET /rankings </summary>
        public static string GetRankings(int heroId) =>
            "rankings" + BuildQuery(("hero_id", heroId.ToString()));
    }
    
    /// <summary> Среднестатистические показатели (бенчмарки) для героя </summary>
    public static class Benchmarks
    {
        /// <summary> Среднестатистические показатели (бенчмарки) для героя. GET /benchmarks </summary>
        public static string GetBenchmarks(int heroId) =>
            "benchmarks" + BuildQuery(("hero_id", heroId.ToString()));
    }
    
    /// <summary> Данные о состоянии сервиса </summary>
    public static class Health
    {
        /// <summary> Данные о состоянии сервиса. GET /health </summary>
        public static string GetHealth() => "health";
    }
    
    /// <summary> Запросы </summary>
    public static class Request
    {
        /// <summary> Получить статус задачи парсинга реплея по ID задачи. GET /request/{jobId} </summary>
        public static string GetRequestStatus(string jobId) => $"request/{jobId}";

        /// <summary> Отправить матч на парсинг реплея (считается как 10 запросов по rate limit). POST /request/{match_id} </summary>
        public static string PostRequestParse(long matchId) => $"request/{matchId}";
    }
    
    /// <summary> Найти последние матчи по наборам героев двух команд </summary>
    public static class FindMatches
    {
        /// <summary> Найти последние матчи по наборам героев двух команд. GET /findMatches </summary>
        public static string GetFindMatches(IEnumerable<int>? teamA = null, IEnumerable<int>? teamB = null)
        {
            var parts = new List<string>();
            if (teamA != null)
                foreach (var id in teamA) parts.Add($"teamA={id}");
            if (teamB != null)
                foreach (var id in teamB) parts.Add($"teamB={id}");
            return parts.Count > 0 ? "findMatches?" + string.Join("&", parts) : "findMatches";
        }
    }
    
    /// <summary> Герои </summary>
    public static class Heroes
    {
        /// <summary> Список всех героев с базовыми атрибутами. GET /heroes </summary>
        public static string GetHeroes() => "heroes";

        /// <summary> Последние матчи, в которых использовался указанный герой. GET /heroes/{hero_id}/matches </summary>
        public static string GetHeroMatches(int heroId) => $"heroes/{heroId}/matches";

        /// <summary> Результаты противостояний героя с другими героями. GET /heroes/{hero_id}/matchups </summary>
        public static string GetHeroMatchups(int heroId) => $"heroes/{heroId}/matchups";

        /// <summary> Статистика героя в зависимости от продолжительности матча. GET /heroes/{hero_id}/durations </summary>
        public static string GetHeroDurations(int heroId) => $"heroes/{heroId}/durations";

        /// <summary> Игроки, которые играли на данном герое. GET /heroes/{hero_id}/players </summary>
        public static string GetHeroPlayers(int heroId) => $"heroes/{heroId}/players";

        /// <summary> Популярность предметов на герое по фазам игры (на основе про-матчей). GET /heroes/{hero_id}/itemPopularity </summary>
        public static string GetHeroItemPopularity(int heroId) => $"heroes/{heroId}/itemPopularity";
    }
    
    /// <summary> Статистика производительности героев </summary>
    public static class HeroStats
    {
        /// <summary> Подробная статистика производительности всех героев в последних матчах. GET /heroStats </summary>
        public static string GetHeroStats() => "heroStats";
    }
    
    /// <summary> Лиги </summary>
    public static class Leagues
    {
        /// <summary> Список всех лиг. GET /leagues </summary>
        public static string GetLeagues() => "leagues";

        /// <summary> Данные конкретной лиги. GET /leagues/{league_id} </summary>
        public static string GetLeague(int leagueId) => $"leagues/{leagueId}";

        /// <summary> Матчи лиги (без любительских лиг). GET /leagues/{league_id}/matches </summary>
        public static string GetLeagueMatches(int leagueId) => $"leagues/{leagueId}/matches";

        /// <summary> ID всех матчей лиги (включая любительские). GET /leagues/{league_id}/matchIds </summary>
        public static string GetLeagueMatchIds(int leagueId) => $"leagues/{leagueId}/matchIds";

        /// <summary> Команды, участвующие в лиге. GET /leagues/{league_id}/teams </summary>
        public static string GetLeagueTeams(int leagueId) => $"leagues/{leagueId}/teams";
    }
    
    /// <summary> Команды </summary>
    public static class Teams
    {
        /// <summary> Список всех команд (постранично, до 1000 на страницу). GET /teams </summary>
        public static string GetTeams(int? page = null) =>
            "teams" + BuildQuery(("page", page?.ToString()));

        /// <summary> Данные конкретной команды. GET /teams/{team_id} </summary>
        public static string GetTeam(int teamId) => $"teams/{teamId}";

        /// <summary> Матчи команды. GET /teams/{team_id}/matches </summary>
        public static string GetTeamMatches(int teamId) => $"teams/{teamId}/matches";

        /// <summary> Игроки, выступавшие за команду. GET /teams/{team_id}/players </summary>
        public static string GetTeamPlayers(int teamId) => $"teams/{teamId}/players";

        /// <summary> Герои, использовавшиеся командой, и их статистика. GET /teams/{team_id}/heroes </summary>
        public static string GetTeamHeroes(int teamId) => $"teams/{teamId}/heroes";
    }
    
    /// <summary> Рекорды </summary>
    public static class Records
    {
        /// <summary> Лучшие индивидуальные результаты по заданной статистике. GET /records/{field} </summary>
        public static string GetRecords(string field) => $"records/{field}";
    }
    
    /// <summary> Текущие live-матчи </summary>
    public static class Live
    {
        /// <summary> Топ текущих live-матчей. GET /live </summary>
        public static string GetLive() => "live";
    }
    
    /// <summary> Сценарии </summary>
    public static class Scenarios
    {
        /// <summary> Процент побед при определённом тайминге покупки предмета на герое. GET /scenarios/itemTimings </summary>
        public static string GetScenarioItemTimings(string? item = null, int? heroId = null) =>
            "scenarios/itemTimings" + BuildQuery(
                ("item",    item),
                ("hero_id", heroId?.ToString())
            );

        /// <summary> Процент побед героя в определённой роли на линии. GET /scenarios/laneRoles </summary>
        public static string GetScenarioLaneRoles(string? laneRole = null, int? heroId = null) =>
            "scenarios/laneRoles" + BuildQuery(
                ("lane_role", laneRole),
                ("hero_id",   heroId?.ToString())
            );

        /// <summary> Различные командные сценарии (teamScenariosQueryParams). GET /scenarios/misc </summary>
        public static string GetScenarioMisc(string? scenario = null) =>
            "scenarios/misc" + BuildQuery(("scenario", scenario));
    }
    
    /// <summary> Схема базы данных OpenDota (таблицы и колонки) </summary>
    public static class Schema
    {
        /// <summary> Схема базы данных OpenDota (таблицы и колонки). GET /schema </summary>
        public static string GetSchema() => "schema";
    }
    
    /// <summary> Статические игровые данные из репозитория dotaconstants (heroes, items, abilities и др.) </summary>
    public static class Constants
    {
        /// <summary> Статические игровые данные из репозитория dotaconstants (heroes, items, abilities и др.). GET /constants/{resource} </summary>
        public static string GetConstants(string resource) => $"constants/{resource}";
    }
    
    private static string BuildQuery(params (string Key, string? Value)[] parameters)
    {
        var parts = parameters
            .Where(p => p.Value is not null)
            .Select(p => $"{p.Key}={p.Value}");

        var query = string.Join("&", parts);
        return query.Length > 0 ? "?" + query : string.Empty;
    }
}