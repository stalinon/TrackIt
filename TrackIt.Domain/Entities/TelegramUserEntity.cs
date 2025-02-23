using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackIt.Domain.Common;

namespace TrackIt.Domain.Entities;

/// <summary>
///     Таблица пользователей телеграмм
/// </summary>
[Table("telegram_users")]
public class TelegramUserEntity : IBaseEntity
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
    ///     Идентификатор пользователя в Телеграмм
    /// </summary>
    [Column("telegram_id")]
    public long TelegramId { get; set; }
    
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
    ///     Настройка сущности
    /// </summary>
    public static void Configure(EntityTypeBuilder<TelegramUserEntity> builder)
    {
        builder.HasKey(tu => tu.Id);

        builder.Property(tu => tu.TelegramId)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(tu => tu.User)
            .WithOne(u => u.TelegramUser)
            .HasForeignKey<TelegramUserEntity>(tu => tu.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
