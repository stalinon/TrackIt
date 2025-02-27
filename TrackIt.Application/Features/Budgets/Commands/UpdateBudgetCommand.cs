using MediatR;
using TrackIt.Application.DTOs.Budgets;

namespace TrackIt.Application.Features.Budgets.Commands;

/// <summary>
///     Запрос на обновление запланированного платежа
/// </summary>
public class UpdateBudgetCommand : IRequest<BudgetDto>
{
    /// <summary>
    ///     Идентификатор запланированного платежа
    /// </summary>
    public Guid BudgetId { get; set; }
    
    /// <summary>
    ///     Величина лимита
    /// </summary>
    public decimal LimitAmount { get; set; }
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; set; }
}