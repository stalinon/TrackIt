using MediatR;
using TrackIt.Application.DTOs.Budgets;

namespace TrackIt.Application.Features.Budgets.Queries;

/// <summary>
///     Запрос на получение платежа по ID.
/// </summary>
public class GetBudgetByIdQuery : IRequest<DetailedBudgetDto?>
{
    /// <summary>
    ///     Идентификатор платежа
    /// </summary>
    public Guid BudgetId { get; set; }
}