namespace TrackIt.Domain.Common;

/// <summary>
///     Интерфейс базовой сущности
/// </summary>
public interface IBaseEntity
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    Guid Id { get; set; }
    
    /// <summary>
    ///     Дата создания
    /// </summary>
    DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///     Дата обновления
    /// </summary>
    DateTime UpdatedAt { get; set; }
}
