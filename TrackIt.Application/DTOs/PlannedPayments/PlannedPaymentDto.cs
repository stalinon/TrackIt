using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.PlannedPayments;

/// <summary>
///     Модель запланированной платы
/// </summary>
public class PlannedPaymentDto
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    ///     Объем оплаты
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    
    /// <summary>
    ///     Дата
    /// </summary>
    [JsonPropertyName("due_date")]
    public DateTime DueDate { get; set; }
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [JsonPropertyName("category_id")]
    public Guid? CategoryId { get; set; }
}