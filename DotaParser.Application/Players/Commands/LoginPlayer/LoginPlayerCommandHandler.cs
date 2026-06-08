using DotaParser.Application.Common.Interfaces;
using DotaParser.Application.Common.Messages;
using DotaParser.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DotaParser.Application.Players.Commands.LoginPlayer;

public class LoginPlayerCommandHandler : IRequestHandler<LoginPlayerCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public LoginPlayerCommandHandler(IApplicationDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(LoginPlayerCommand request, CancellationToken cancellationToken)
    {
        // Конвертируем 64-битный SteamID в 32-битный AccountId для Dota 2
        long accountId = request.SteamId64 - 76561197960265728;

        // Ищем игрока в БД
        var player = await _context.Players
            .FirstOrDefaultAsync(p => p.SteamAccountId == accountId, cancellationToken);

        if (player == null)
        {
            // Если новый - создаем
            player = new Player
            {
                Id = Guid.NewGuid(),
                SteamAccountId = accountId,
                PersonaName = request.PersonaName,
                AvatarUrl = request.AvatarUrl,
                ProfileUrl = $"https://steamcommunity.com/profiles/{request.SteamId64}"
            };
            _context.Players.Add(player);
        }
        else
        {
            // Если уже есть - обновляем ник и аватар (они могли поменяться в Steam)
            player.PersonaName = request.PersonaName;
            player.AvatarUrl = request.AvatarUrl;
        }

        await _context.SaveChangesAsync(cancellationToken);

        // Отправляем сообщение в RabbitMQ асинхронно
        // Консьюмеры (обработчики) в другом месте поймают его и сделают нужную работу
        await _publishEndpoint.Publish(new PlayerLoggedInEvent
        {
            AccountId = accountId,
            LoggedInAt = DateTime.UtcNow
        }, cancellationToken);

        return player.Id; // Возвращаем наш внутренний ID
    }
}