namespace TrackIt.Application.DTOs.Budgets;

/// <summary>
///     Детализированная модель лимита бюджета
/// </summary>
public class DetailedBudgetDto : BudgetDto
{
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///     Дата обновления
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}