using DotaParser.Application.Common.DTOs;
using DotaParser.Application.Common.Interfaces;
using DotaParser.Infrastructure.Constants;
using DotaParser.Infrastructure.Models;
using System.Net.Http.Json;

namespace DotaParser.Infrastructure.Services;

public class OpenDotaService : IOpenDotaService
{
    private readonly HttpClient _httpClient;

    public OpenDotaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PlayerProfileDto?> GetPlayerProfileAsync(long accountId, CancellationToken cancellationToken)
    {
        // Делаем GET-запрос: https://api.opendota.com/api/players/{accountId}
        var endpoint = OpenDotaEndpoints.PlayerProfile(accountId);
        var response = await _httpClient.GetAsync(endpoint, cancellationToken);

        // Если профиль не найден (или скрыт), OpenDota может вернуть 404
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        // Если вылезла ошибка (например, 429 Too Many Requests), выбрасываем исключение.
        // Дальше его перехватит политика Resilience (повторы запросов).
        response.EnsureSuccessStatusCode();

        // Читаем JSON в нашу инфраструктурную модель
        var data = await response.Content.ReadFromJsonAsync<OpenDotaPlayerResponse>(cancellationToken: cancellationToken);

        if (data?.Profile == null)
            return null;

        // Маппинг инфраструктурной модели во внутренний DTO слоя Application
        return new PlayerProfileDto
        {
            AccountId = data.Profile.AccountId,
            PersonaName = data.Profile.PersonaName,
            AvatarFull = data.Profile.AvatarFull,
            LocCountryCode = data.Profile.LocCountryCode,
            IsDotaPlusSubscriber = data.Profile.Plus ?? false,
            RankTier = data.RankTier,
            LeaderboardRank = data.LeaderboardRank,
            ComputedMmr = data.ComputedMmr
        };
    }

    public async Task<List<MatchDto>> GetPlayerMatchesAsync(long accountId, int limit, CancellationToken cancellationToken)
    {
        // Запрос к: https://api.opendota.com/api/players/{account_id}/matches?limit=20
        var endpoint = OpenDotaEndpoints.PlayerMatches(accountId, limit);
        var response = await _httpClient.GetAsync(endpoint, cancellationToken);

        response.EnsureSuccessStatusCode(); // Наш ResilienceHandler обработает 429 ошибку (Rate limit) сам

        var matches = await response.Content.ReadFromJsonAsync<List<OpenDotaMatchResponse>>(cancellationToken: cancellationToken);

        if (matches == null) return new List<MatchDto>();

        return matches.Select(m => new MatchDto
        {
            MatchId = m.MatchId,
            PlayerSlot = m.PlayerSlot,
            RadiantWin = m.RadiantWin,
            DurationSeconds = m.Duration,
            GameMode = m.GameMode,
            LobbyType = m.LobbyType,
            HeroId = m.HeroId,
            StartTime = m.StartTime,
            Kills = m.Kills,
            Deaths = m.Deaths,
            Assists = m.Assists,
            AverageRank = m.AverageRank,
            LeaverStatus = m.LeaverStatus,
            PartySize = m.PartySize
        }).ToList();
    }
}