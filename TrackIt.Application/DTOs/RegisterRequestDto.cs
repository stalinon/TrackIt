namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO для запроса на регистрацию.
/// </summary>
public class RegisterRequestDto
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