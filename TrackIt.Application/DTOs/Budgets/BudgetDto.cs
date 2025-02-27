namespace TrackIt.Application.DTOs.Budgets;

/// <summary>
///     Модель лимита бюджета
/// </summary>
public class BudgetDto
{
    /// <summary>
    ///     Величина лимита
    /// </summary>
    public decimal LimitAmount { get; set; }
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; set; }
}