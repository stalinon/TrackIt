using MediatR;
using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Domain.Common;

namespace TrackIt.Application.Features.PlannedPayments.Queries;

/// <summary>
///     Запрос на получение категорий.
/// </summary>
public class GetPlannedPaymentQuery : PagedQuery, IRequest<IEnumerable<PlannedPaymentDto>>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; set; }
}