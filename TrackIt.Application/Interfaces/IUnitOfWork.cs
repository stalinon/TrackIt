using TrackIt.Application.Interfaces.Repositories;

namespace TrackIt.Application.Interfaces;

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
    ///     Транзакции
    /// </summary>
    ITransactionRepository Transactions { get; }
    
    /// <summary>
    ///     Сохранить изменения
    /// </summary>
    Task<int> SaveChangesAsync();
}