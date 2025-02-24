namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO для регистрации пользователя.
/// </summary>
public class RegisterDto
{
    /// <summary>
    ///     Адрес электронной почты пользователя.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    ///     Пароль пользователя.
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    ///     Подтверждение пароля пользователя.
    /// </summary>
    public string ConfirmPassword { get; set; } = default!;
}