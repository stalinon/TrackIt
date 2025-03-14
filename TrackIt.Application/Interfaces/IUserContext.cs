using TrackIt.Application.DTOs;
using TrackIt.Domain.Entities;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Контекст пользователей
/// </summary>
public interface IUserContext
{
    /// <summary>
    ///     Идентификатор пользователя
    /// </summary>
    Guid UserId { get; }
    
    /// <summary>
    ///     Почта пользователя
    /// </summary>
    string? Email { get; }
    
    /// <summary>
    ///     Роли пользователя
    /// </summary>
    IEnumerable<string> Roles { get; }
    
    /// <summary>
    ///     Создать пользователя, если его нет в БД
    /// </summary>
    Task<UserDto> GetOrCreateCurrentUserAsync();
    
    /// <summary>
    ///     Авторизовать пользователя телеграма
    /// </summary>
    Task AuthorizeTelegramUserAsync(long telegramId);
}