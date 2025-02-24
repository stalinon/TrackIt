namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO для входа пользователя в систему.
/// </summary>
public class LoginDto
{
    /// <summary>
    ///     Адрес электронной почты пользователя.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    ///     Пароль пользователя.
    /// </summary>
    public string Password { get; set; } = default!;
}