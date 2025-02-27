using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Budgets;

/// <summary>
///     Модель лимита бюджета
/// </summary>
public class BudgetDto
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    ///     Величина лимита
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal LimitAmount { get; set; }
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [JsonPropertyName("category_id")]
    public Guid? CategoryId { get; set; }
}