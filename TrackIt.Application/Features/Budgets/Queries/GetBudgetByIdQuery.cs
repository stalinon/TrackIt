using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    [FromQuery(Name = "id")]
    public Guid BudgetId { get; set; }
}