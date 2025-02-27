using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackIt.Domain.Common;

namespace TrackIt.Domain.Entities;

/// <summary>
///     Таблица запланированных плат
/// </summary>
[Table("planned_payments")]
public class PlannedPaymentEntity : IBaseEntity
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
    ///     Количество
    /// </summary>
    [Column("amount")]
    public decimal Amount { get; set; }
    
    /// <summary>
    ///     Дата
    /// </summary>
    [Column("due_date")]
    public DateTime DueDate { get; set; }
    
    /// <summary>
    ///     Описание
    /// </summary>
    [Column("description")]
    public string Description { get; set; } = default!;

    /// <summary>
    ///     Идентификатор расписания
    /// </summary>
    [Column("schedule_id")]
    public string? ScheduleId { get; set; }
    
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
    public static void Configure(EntityTypeBuilder<PlannedPaymentEntity> builder)
    {
        builder.HasKey(pp => pp.Id);

        builder.Property(pp => pp.Amount)
            .IsRequired();

        builder.Property(pp => pp.DueDate)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(pp => pp.User)
            .WithMany(u => u.PlannedPayments)
            .HasForeignKey(pp => pp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pp => pp.Category)
            .WithMany(u => u.PlannedPayments)
            .HasForeignKey(pp => pp.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
