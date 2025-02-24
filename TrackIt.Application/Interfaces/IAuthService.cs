using TrackIt.Application.DTOs;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Интерфейс сервиса аутентификации
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Зарегистрироваться
    /// </summary>
    /// <param name="request">Запрос на регистрацию</param>
    /// <returns>Ответ от сервиса</returns>
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    
    /// <summary>
    ///     Залогиниться
    /// </summary>
    /// <param name="request">Запрос на вход</param>
    /// <returns>Ответ от сервиса</returns>
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    
    /// <summary>
    ///     Обновить токен
    /// </summary>
    /// <param name="request">Запрос на обновление токена</param>
    /// <returns>Ответ от сервиса</returns>
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
}