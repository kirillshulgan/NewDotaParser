using DotaParser.Application.Common.Interfaces;
using DotaParser.Application.Common.Messages;
using DotaParser.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotaParser.Application.Players.EventHandlers;

/// <summary>
/// Фоновый обработчик события входа игрока. 
/// Подписан на очередь RabbitMQ.
/// </summary>
public class PlayerLoggedInEventConsumer : IConsumer<PlayerLoggedInEvent>
{
    private readonly ILogger<PlayerLoggedInEventConsumer> _logger;
    private readonly IOpenDotaService _openDotaService;
    private readonly IApplicationDbContext _context;

    public PlayerLoggedInEventConsumer(ILogger<PlayerLoggedInEventConsumer> logger, IOpenDotaService openDotaService, IApplicationDbContext context)
    {
        _logger = logger;
        _openDotaService = openDotaService;
        _context = context;
    }

    public async Task Consume(ConsumeContext<PlayerLoggedInEvent> context)
    {
        var message = context.Message;

        try
        {
            var fetchedMatches = await _openDotaService.GetPlayerMatchesAsync(message.AccountId, 100, context.CancellationToken);

            if (!fetchedMatches.Any())
                return;

            // Достаем из базы ID тех матчей, которые мы УЖЕ сохраняли ранее,
            // чтобы не добавлять дубликаты.
            var existingMatchIds = await _context.Matches
                .Where(m => (m.RadiantTeam != null && m.RadiantTeam.Players.Any(x => x.AccountId == message.AccountId)) || ( m.DireTeam != null && m.DireTeam.Players.Any(x => x.AccountId == message.AccountId)))
                .Select(m => m.MatchId)
                .ToListAsync(context.CancellationToken);

            var newMatchesToSave = new List<Match>();

            foreach (var dto in fetchedMatches)
            {
                if (existingMatchIds.Contains(dto.MatchId))
                    continue; // Пропускаем дубликаты

                newMatchesToSave.Add(new Match
                {
                    Id = Guid.NewGuid(),
                    MatchId = dto.MatchId,
                    RadiantWin = dto.RadiantWin,
                    Duration = dto.DurationSeconds,
                    GameMode = dto.GameMode,
                    LobbyType = dto.LobbyType,
                    StartTime = DateTimeOffset.FromUnixTimeSeconds(dto.StartTime).UtcDateTime
                });
            }

            if (newMatchesToSave.Any())
            {
                await _context.Matches.AddRangeAsync(newMatchesToSave, context.CancellationToken);
                await _context.SaveChangesAsync(context.CancellationToken);

                _logger.LogInformation("Успешно сохранено {Count} новых матчей для AccountId: {AccountId}",
                    newMatchesToSave.Count, message.AccountId);
            }
            else
            {
                _logger.LogInformation("Новых матчей для AccountId: {AccountId} не найдено", message.AccountId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при загрузке и сохранении матчей для {AccountId}", message.AccountId);
            throw;
        }
    }
}