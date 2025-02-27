using MediatR;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Domain.Common;

namespace TrackIt.Application.Features.Budgets.Queries;

/// <summary>
///     Запрос на получение лимита.
/// </summary>
public class GetBudgetQuery : PagedQuery, IRequest<IEnumerable<BudgetDto>>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; set; }
}