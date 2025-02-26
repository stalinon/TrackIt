using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackIt.Domain.Common;
using TrackIt.Domain.Enums;

namespace TrackIt.Domain.Entities;

/// <summary>
///     Таблица категорий
/// </summary>
[Table("categories")]
public class CategoryEntity : IBaseEntity
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
    ///     Название категории
    /// </summary>
    [Column("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    ///     Тип категории
    /// </summary>
    /// <remarks>income / expense</remarks>
    [Column("type")]
    public CategoryType Type { get; set; }
    
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
    ///     Транзакции
    /// </summary>
    public ICollection<TransactionEntity> Transactions { get; set; } = default!;
    
    /// <summary>
    ///     Бюджеты
    /// </summary>
    public ICollection<BudgetEntity> Budgets { get; set; } = default!;

    /// <summary>
    ///     Настройка сущности
    /// </summary>
    public static void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Type)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => new {c.UserId, c.Name, c.Type})
            .IsUnique()
            .HasDatabaseName("IX_categories_unique");
    }
}
