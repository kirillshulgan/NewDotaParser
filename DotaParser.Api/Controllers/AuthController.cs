using DotaParser.Application.Players.Commands.LoginPlayer;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DotaParser.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Эндпоинт, который перенаправляет пользователя на страницу входа Steam.
    /// </summary>
    [HttpGet("login")]
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties { RedirectUri = "/api/auth/callback" };
        return Challenge(properties, "Steam");
    }

    /// <summary>
    /// Сюда Steam вернет пользователя после успешного входа.
    /// </summary>
    [HttpGet("callback")]
    public async Task<IActionResult> Callback(CancellationToken cancellationToken)
    {
        // Читаем данные, которые вернул Steam
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
            return Unauthorized("Авторизация не удалась");

        // Steam возвращает Claim с полным URL профиля (например: https://steamcommunity.com/openid/id/76561198012345678)
        var claimIdId = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var name = authenticateResult.Principal.Identity?.Name ?? "Unknown";

        if (string.IsNullOrEmpty(claimIdId))
            return BadRequest("Не удалось получить Steam ID");

        // Достаем только цифры из URL
        var steamId64String = claimIdId.Split('/').Last();
        if (!long.TryParse(steamId64String, out var steamId64))
            return BadRequest("Неверный формат Steam ID");

        // Отправляем команду в Application слой
        // Примечание: Аватарку Steam OpenID 2.0 по умолчанию не отдает, поэтому пока передаем пустое поле (ее можно догрузить через Steam Web API позже)
        var command = new LoginPlayerCommand(steamId64, name, string.Empty);
        var playerId = await _mediator.Send(command, cancellationToken);

        return Ok(new { Message = "Успешный вход!", InternalPlayerId = playerId, SteamName = name });
    }

    /// <summary>
    /// Эндпоинт для выхода.
    /// </summary>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { Message = "Вы вышли из системы" });
    }
}