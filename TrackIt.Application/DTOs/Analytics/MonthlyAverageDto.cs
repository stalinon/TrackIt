using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Analytics;

/// <summary>
///     Модель средних затрат
/// </summary>
public class MonthlyAverageDto
{
    /// <summary>
    ///     Средние затраты
    /// </summary>
    [JsonPropertyName("average_month_spent")]
    public decimal AverageMonthlySpending { get; set; }
}