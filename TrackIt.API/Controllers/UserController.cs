using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs;
using TrackIt.Application.Features.Users.Commands;
using TrackIt.Application.Interfaces;
using TrackIt.TelegramBot.BotCommands;

namespace TrackIt.API.Controllers;

/// <summary>
///     Контроллер пользователей
/// </summary>
[Route("api/users")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMediator _mediator;

    /// <inheritdoc cref="UserController"/>
    public UserController(IUserService userService, IMediator mediator)
    {
        _userService = userService;
        _mediator = mediator;
    }

    private string? GetUserEmail()
    {
        return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
               ?? User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
    }

    /// <summary>
    ///     Получить профиль текущего пользователя
    /// </summary>
    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserDto), 200)]
    public async Task<IActionResult> GetUserProfile()
    {
        var userEmail = GetUserEmail();
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("Email not found in token");
        }

        var user = await _userService.GetUserByEmailAsync(userEmail);
        return user == null ? NotFound("User not found") : Ok(user);
    }

    /// <summary>
    ///     Привязать пользователя Телеграм
    /// </summary>
    [HttpPost("link")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> LinkTelegram(LinkUserToTelegramCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}