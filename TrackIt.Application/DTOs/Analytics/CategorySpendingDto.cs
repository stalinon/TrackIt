namespace TrackIt.Application.DTOs.Analytics;

/// <summary>
///     Модель траты по категории
/// </summary>
public class CategorySpendingDto
{
    /// <summary>
    ///     Название категории
    /// </summary>
    public string CategoryName { get; set; } = default!;
    
    /// <summary>
    ///     Всего потрачено
    /// </summary>
    public decimal TotalSpent { get; set; }
}