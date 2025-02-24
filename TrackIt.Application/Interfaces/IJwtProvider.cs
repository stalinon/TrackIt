using TrackIt.Application.DTOs;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Интерфейс для генерации и валидации JWT-токенов.
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    ///     Сгенерировать асес-токен
    /// </summary>
    /// <param name="user">Модель пользователя</param>
    /// <returns>Асес-токен</returns>
    string GenerateAccessToken(UserDto user);
    
    /// <summary>
    ///     Сгенерировать рефреш-токен
    /// </summary>
    /// <param name="user">Модель пользователя</param>
    /// <returns>Рефреш токен</returns>
    RefreshTokenDto GenerateRefreshToken(UserDto user);
    
    /// <summary>
    ///     Валидировать токен
    /// </summary>
    /// <param name="token">Токен</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Прошел ли токен валидацию</returns>
    bool ValidateToken(string token, out Guid userId);
}
