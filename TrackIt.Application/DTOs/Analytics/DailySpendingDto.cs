using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Analytics;

/// <summary>
///     Модель дневных трат
/// </summary>
public class DailySpendingDto
{
    /// <summary>
    ///     Номер дня
    /// </summary>
    [JsonPropertyName("day")]
    public int Day { get; set; }
    
    /// <summary>
    ///     Всего потрачено
    /// </summary>
    [JsonPropertyName("total_spent")]
    public decimal TotalSpent { get; set; }
}