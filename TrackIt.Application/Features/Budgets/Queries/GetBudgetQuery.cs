using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Domain.Common;

namespace TrackIt.Application.Features.Budgets.Queries;

/// <summary>
///     Запрос на получение лимита.
/// </summary>
public class GetBudgetQuery : PagedQuery, IRequest<PagedList<BudgetDto>>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [FromQuery(Name = "category_id")]
    public Guid? CategoryId { get; set; }
}