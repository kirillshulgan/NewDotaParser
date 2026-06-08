using DotaParser.Application.Common.DTOs;
using DotaParser.Application.Common.Interfaces;
using MediatR;

namespace DotaParser.Application.Players.Queries.GetPlayerProfile;

public class GetPlayerProfileQueryHandler : IRequestHandler<GetPlayerProfileQuery, PlayerProfileDto?>
{
    private readonly IOpenDotaService _openDotaService;

    public GetPlayerProfileQueryHandler(IOpenDotaService openDotaService)
    {
        _openDotaService = openDotaService;
    }

    public async Task<PlayerProfileDto?> Handle(GetPlayerProfileQuery request, CancellationToken cancellationToken)
    {
        // Пока просто обращаемся к сервису. Кэширование (Redis) мы добавим позже через MediatR PipelineBehavior!
        return await _openDotaService.GetPlayerProfileAsync(request.AccountId, cancellationToken);
    }
}