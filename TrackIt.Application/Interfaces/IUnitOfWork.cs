namespace TrackIt.Application.Interfaces;

/// <summary>
///     Фасад из репозиториев
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    ///     Получить репозиторий
    /// </summary>
    IRepository<T> Repository<T>() where T : class;
    
    /// <summary>
    ///     Сохранить контекст
    /// </summary>
    Task<int> SaveChangesAsync();
}