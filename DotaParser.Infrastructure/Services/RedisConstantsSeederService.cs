using DotaParser.Application.Common.Models.Constants;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DotaParser.Infrastructure.Services;

/// <summary>
/// Фоновый сервис, который запускается ДО готовности API принимать запросы.
/// Он читает локальные JSON файлы констант и загружает их в Redis.
/// </summary>
public class RedisConstantsSeederService : IHostedService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisConstantsSeederService> _logger;

    public RedisConstantsSeederService(IDistributedCache cache, ILogger<RedisConstantsSeederService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начинаем загрузку констант Dota 2 в Redis...");

        try
        {
            await SeedHeroesAsync(cancellationToken);
            // По аналогии можно добавить SeedGameModesAsync, SeedLobbyTypesAsync и т.д.

            _logger.LogInformation("Загрузка констант в Redis успешно завершена.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при загрузке констант в Redis.");
            // Мы не пробрасываем ошибку дальше, чтобы API все равно запустилось,
            // но в реальности здесь можно кинуть throw, если эти данные критичны для старта.
        }
    }

    private async Task SeedHeroesAsync(CancellationToken cancellationToken)
    {
        var heroesJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Constants", "Files", "heroes.json");

        if (!File.Exists(heroesJsonPath))
        {
            _logger.LogWarning("Файл {Path} не найден. Пропуск.", heroesJsonPath);
            return;
        }

        var json = await File.ReadAllTextAsync(heroesJsonPath, cancellationToken);
        var heroesDict = JsonSerializer.Deserialize<Dictionary<string, HeroConstant>>(json);

        if (heroesDict == null) return;

        // Сохраняем каждый объект героя в Redis.
        // Ключ будет в формате: "constant_hero_1", "constant_hero_2" и т.д.
        // Так как константы не меняются до следующего патча, задаем большое время жизни кэша (или делаем бессрочным)
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
        };

        foreach (var (heroId, heroData) in heroesDict)
        {
            var cacheKey = $"constant_hero_{heroId}";

            // Если героя еще нет в кэше — добавляем
            var existingHero = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if (string.IsNullOrEmpty(existingHero))
            {
                var serializedHero = JsonSerializer.Serialize(heroData);
                await _cache.SetStringAsync(cacheKey, serializedHero, options, cancellationToken);
            }
        }

        _logger.LogInformation("Загружено {Count} героев в Redis.", heroesDict.Count);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}