using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackIt.Domain.Common;
using TrackIt.Domain.Enums;

namespace TrackIt.Domain.Entities;

/// <summary>
///     Таблица лимитов бюджета
/// </summary>
[Table("budgets")]
public class BudgetEntity : IBaseEntity
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
    ///     Величина лимита
    /// </summary>
    [Column("limit_amount")]
    public decimal LimitAmount { get; set; }

    /// <summary>
    ///     Период
    /// </summary>
    [Column("period")]
    public BudgetPeriod Period { get; set; }
    
    /// <summary>
    ///     Идентификатор пользователя
    /// </summary>
    [Column("user_id")]
    public Guid UserId { get; set; }

    /// <summary>
    ///     Пользователь
    /// </summary>
    public UserEntity User { get; set; } = default!;
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [Column("category_id")]
    public Guid? CategoryId { get; set; }

    /// <summary>
    ///     Категория
    /// </summary>
    public CategoryEntity Category { get; set; } = default!;

    /// <summary>
    ///     Настройка сущности
    /// </summary>
    public static void Configure(EntityTypeBuilder<BudgetEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.LimitAmount)
            .IsRequired();

        builder.Property(b => b.Period)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(b => b.User)
            .WithMany(u => u.Budgets)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Category)
            .WithMany(c => c.Budgets)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
