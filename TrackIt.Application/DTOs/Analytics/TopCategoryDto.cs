namespace TrackIt.Application.DTOs.Analytics;

/// <summary>
///     Модель самых затратных категорий
/// </summary>
public class TopCategoryDto
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