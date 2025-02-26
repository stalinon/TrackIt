using System.Linq.Expressions;

namespace TrackIt.Application.Interfaces.Repositories;

/// <summary>
///     Репозиторий
/// </summary>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    ///     Получить сущность по идентификатору
    /// </summary>
    Task<T?> GetByIdAsync(Guid id);
    
    /// <summary>
    ///     Получить все сущности
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    ///     Найти сущности
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    ///     Получить пагинированный список сущностей (сортировки, фильтрации и прочее)
    /// </summary>
    Task<IEnumerable<T>> GetPaginatedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false);
    
    /// <summary>
    ///     Существуют ли сущности
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    ///     Посчитать сущности
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    
    /// <summary>
    ///     Добавить сущности
    /// </summary>
    Task AddAsync(T entity);
    
    /// <summary>
    ///     Обновить сущность
    /// </summary>
    void Update(T entity);
    
    /// <summary>
    ///     Удалить сущность
    /// </summary>
    void Remove(T entity);
}
