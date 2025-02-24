namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO для запроса на авторизацию.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    ///     Электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    ///     Пароль пользователя.
    /// </summary>
    public string Password { get; set; } = default!;
}