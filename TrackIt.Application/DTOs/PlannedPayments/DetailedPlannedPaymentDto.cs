using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.PlannedPayments;

/// <summary>
///     Детализированная модель запланированной платы
/// </summary>
public class DetailedPlannedPaymentDto : PlannedPaymentDto
{
    /// <summary>
    ///     Описание
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
    
    /// <summary>
    ///     Дата создания
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///     Дата обновления
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}