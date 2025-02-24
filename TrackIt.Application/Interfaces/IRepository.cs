using System.Linq.Expressions;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Репозиторий
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    ///     Получить по id
    /// </summary>
    Task<T?> GetByIdAsync(Guid id);
    
    /// <summary>
    ///     Получить все сущности
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    ///     Добавить сущность
    /// </summary>
    Task<T> AddAsync(T entity);
    
    /// <summary>
    ///     Обновить сущность
    /// </summary>
    Task UpdateAsync(T entity);
    
    /// <summary>
    ///     Удалить сущность
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    ///     Поиск сущностей (фильтрация)
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    ///     Пагинация, сортировка, фильтрация
    /// </summary>
    Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
}
