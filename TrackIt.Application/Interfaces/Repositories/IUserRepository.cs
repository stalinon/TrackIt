using TrackIt.Domain.Entities;

namespace TrackIt.Application.Interfaces.Repositories;

/// <summary>
///     Репозиторий пользователей
/// </summary>
public interface IUserRepository : IGenericRepository<UserEntity>
{
    /// <summary>
    ///     Получить пользователя по почте
    /// </summary>
    Task<UserEntity?> GetByEmailAsync(string email);
    
    /// <summary>
    ///     Получить пагинированный список пользователей
    /// </summary>
    Task<IEnumerable<UserEntity>> GetPaginatedUsersAsync(int pageIndex, int pageSize);
}