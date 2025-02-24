namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO для запроса обновления токена.
/// </summary>
public class RefreshTokenRequestDto
{
    /// <summary>
    ///     Текущий рефреш-токен.
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}