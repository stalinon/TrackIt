using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackIt.Domain.Common;

namespace TrackIt.Domain.Entities;

/// <summary>
///     Таблица пользователей
/// </summary>
[Table("users")]
public class UserEntity : IBaseEntity
{
    /// <inheritdoc />
    [Column("id")]
    public Guid Id { get; set; }
    
    /// <inheritdoc />
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    /// <inheritdoc />
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Электронная почта пользователя
    /// </summary>
    [Column("email")]
    public string Email { get; set; } = default!;
    
    /// <summary>
    ///     Категории
    /// </summary>
    public ICollection<CategoryEntity> Categories { get; set; } = default!;
    
    /// <summary>
    ///     Транзакции
    /// </summary>
    public ICollection<TransactionEntity> Transactions { get; set; } = default!;
    
    /// <summary>
    ///     Запланированные оплаты
    /// </summary>
    public ICollection<PlannedPaymentEntity> PlannedPayments { get; set; } = default!;
    
    /// <summary>
    ///     Бюджеты
    /// </summary>
    public ICollection<BudgetEntity> Budgets { get; set; } = default!;
    
    /// <summary>
    ///     Телеграм
    /// </summary>
    public TelegramUserEntity TelegramUser { get; set; } = default!;

    /// <summary>
    ///     Настройка сущности
    /// </summary>
    public static void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(u => u.Categories)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Transactions)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.PlannedPayments)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Budgets)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.TelegramUser)
            .WithOne(t => t.User)
            .HasForeignKey<TelegramUserEntity>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
