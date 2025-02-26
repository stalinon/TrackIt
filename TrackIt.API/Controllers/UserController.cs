using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.Interfaces;

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

    /// <inheritdoc cref="UserController"/>
    public UserController(IUserService userService)
    {
        _userService = userService;
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
}