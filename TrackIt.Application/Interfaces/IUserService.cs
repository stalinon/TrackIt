using TrackIt.Application.DTOs;
using TrackIt.Domain.Entities;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Сервис пользователей
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Получить пользователя по его почте
    /// </summary>
    Task<UserDto?> GetUserByEmailAsync(string email);
}