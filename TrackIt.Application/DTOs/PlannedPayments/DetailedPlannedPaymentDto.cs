namespace TrackIt.Application.DTOs.PlannedPayments;

/// <summary>
///     Детализированная модель запланированной платы
/// </summary>
public class DetailedPlannedPaymentDto : PlannedPaymentDto
{
    /// <summary>
    ///     Описание
    /// </summary>
    public string Description { get; set; } = default!;
    
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///     Дата обновления
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}