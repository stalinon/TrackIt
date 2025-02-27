namespace TrackIt.Application.DTOs.Analytics;

/// <summary>
///     Модель дневных трат
/// </summary>
public class DailySpendingDto
{
    /// <summary>
    ///     Номер дня
    /// </summary>
    public int Day { get; set; }
    
    /// <summary>
    ///     Всего потрачено
    /// </summary>
    public decimal TotalSpent { get; set; }
}