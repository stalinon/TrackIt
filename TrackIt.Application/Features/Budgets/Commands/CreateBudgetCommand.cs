using MediatR;
using TrackIt.Application.DTOs.Budgets;

namespace TrackIt.Application.Features.Budgets.Commands;

/// <summary>
///     Запрос на создание запланированного платежа
/// </summary>
public class CreateBudgetCommand : IRequest<BudgetDto>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    ///     Величина лимита
    /// </summary>
    public decimal LimitAmount { get; set; }
}