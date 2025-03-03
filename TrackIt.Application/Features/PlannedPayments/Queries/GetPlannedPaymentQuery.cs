using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Domain.Common;

namespace TrackIt.Application.Features.PlannedPayments.Queries;

/// <summary>
///     Запрос на получение категорий.
/// </summary>
public class GetPlannedPaymentQuery : PagedQuery, IRequest<PagedList<PlannedPaymentDto>>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [FromQuery(Name = "category_id")]
    public Guid? CategoryId { get; set; }
}