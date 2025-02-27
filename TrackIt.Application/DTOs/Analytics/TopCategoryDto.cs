using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Analytics;

/// <summary>
///     Модель самых затратных категорий
/// </summary>
public class TopCategoryDto
{
    /// <summary>
    ///     Название категории
    /// </summary>
    [JsonPropertyName("category")]
    public string CategoryName { get; set; } = default!;
    
    /// <summary>
    ///     Всего потрачено
    /// </summary>
    [JsonPropertyName("total_spent")]
    public decimal TotalSpent { get; set; }
}