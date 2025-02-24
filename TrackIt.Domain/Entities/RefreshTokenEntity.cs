using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackIt.Domain.Common;

namespace TrackIt.Domain.Entities;

/// <summary>
///     Таблица рефреш-токенов
/// </summary>
[Table("refresh_tokens")]
public class RefreshTokenEntity : IBaseEntity
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
    ///     Токен
    /// </summary>
    [Column("token")]
    public string Token { get; set; } = default!;
    
    /// <summary>
    ///     Дата экспирации
    /// </summary>
    [Column("expiry_date")]
    public DateTime ExpiryDate { get; set; }
    
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
    public static void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(b => b.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}