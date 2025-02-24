namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO для обновления JWT-токена.
/// </summary>
public class RefreshTokenDto
{
    /// <summary>
    ///     JWT-токен обновления.
    /// </summary>
    public string RefreshToken { get; set; } = default!;

    /// <summary>
    ///     Дата экспирации
    /// </summary>
    public DateTime ExpiryTime { get; set; } = default!;
}