using DotaParser.Application.Players.Queries.GetPlayerProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotaParser.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlayersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetProfile(long accountId, CancellationToken cancellationToken)
    {
        // Создаем запрос
        var query = new GetPlayerProfileQuery(accountId);

        // Отправляем в MediatR. Он сам найдет наш GetPlayerProfileQueryHandler
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound(new { Message = $"Игрок с AccountId {accountId} не найден." });

        return Ok(result);
    }
}