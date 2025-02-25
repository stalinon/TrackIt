using TrackIt.Domain.Interfaces.Repositories;

namespace TrackIt.Domain.Interfaces;

/// <summary>
///     Паттерн UnitOfWork
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    ///     Пользователи
    /// </summary>
    IUserRepository Users { get; }
    
    /// <summary>
    ///     Сохранить изменения
    /// </summary>
    Task<int> SaveChangesAsync();
}