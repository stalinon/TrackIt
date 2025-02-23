using Microsoft.EntityFrameworkCore;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Persistence;

/// <summary>
///     Контекст базы данных
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    ///     Таблица пользователей
    /// </summary>
    public DbSet<UserEntity> Users { get; set; }
    
    /// <summary>
    ///     Таблица категорий
    /// </summary>
    public DbSet<CategoryEntity> Categories { get; set; }
    
    /// <summary>
    ///     Таблица транзакций
    /// </summary>
    public DbSet<TransactionEntity> Transactions { get; set; }
    
    /// <summary>
    ///     Таблица запланированных оплат
    /// </summary>
    public DbSet<PlannedPaymentEntity> PlannedPayments { get; set; }
    
    /// <summary>
    ///     Таблица лимитов бюджета
    /// </summary>
    public DbSet<BudgetEntity> Budgets { get; set; }
    
    /// <summary>
    ///     Таблица пользователей телеграмм
    /// </summary>
    public DbSet<TelegramUserEntity> TelegramUsers { get; set; }

    /// <inheritdoc cref="ApplicationDbContext" />
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация каждой сущности
        UserEntity.Configure(modelBuilder.Entity<UserEntity>());
        CategoryEntity.Configure(modelBuilder.Entity<CategoryEntity>());
        TransactionEntity.Configure(modelBuilder.Entity<TransactionEntity>());
        PlannedPaymentEntity.Configure(modelBuilder.Entity<PlannedPaymentEntity>());
        BudgetEntity.Configure(modelBuilder.Entity<BudgetEntity>());
        TelegramUserEntity.Configure(modelBuilder.Entity<TelegramUserEntity>());
    }
}
