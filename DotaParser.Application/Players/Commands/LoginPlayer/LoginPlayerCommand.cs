using MediatR;

namespace DotaParser.Application.Players.Commands.LoginPlayer;

public record LoginPlayerCommand(long SteamId64, string PersonaName, string AvatarUrl) : IRequest<Guid>;