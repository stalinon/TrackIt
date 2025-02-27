using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Budgets;

/// <summary>
///     Детализированная модель лимита бюджета
/// </summary>
public class DetailedBudgetDto : BudgetDto
{
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