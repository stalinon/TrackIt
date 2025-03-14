using Microsoft.EntityFrameworkCore.Storage;
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
    ///     Категории
    /// </summary>
    ICategoryRepository Categories { get; }
    
    /// <summary>
    ///     Запланированные платежи
    /// </summary>
    IPlannedPaymentRepository PlannedPayments { get; }
    
    /// <summary>
    ///     Лимиты бюджета
    /// </summary>
    IBudgetRepository Budgets { get; }
    
    /// <summary>
    ///     Пользователи Телеграма
    /// </summary>
    ITelegramUserRepository TelegramUsers { get; }
    
    /// <summary>
    ///     Сохранить изменения
    /// </summary>
    Task<int> SaveChangesAsync();
    
    /// <summary>
    ///     Начать транзакцию
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    ///     Закоммитить транзакцию
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    ///     Откатить транзакцию
    /// </summary>
    Task RollbackTransactionAsync();
}