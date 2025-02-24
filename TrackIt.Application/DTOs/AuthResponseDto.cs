namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO для ответа с токенами авторизации.
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    ///     JWT-токен доступа.
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    ///     Рефреш-токен.
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}