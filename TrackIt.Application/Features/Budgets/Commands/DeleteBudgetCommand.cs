using MediatR;

namespace TrackIt.Application.Features.Budgets.Commands;

/// <summary>
///     Запрос на удаление запланированного платежа
/// </summary>
public class DeleteBudgetCommand : IRequest<bool>
{
    /// <summary>
    ///     Идентификатор запланированного платежа
    /// </summary>
    public Guid BudgetId { get; set; }
}