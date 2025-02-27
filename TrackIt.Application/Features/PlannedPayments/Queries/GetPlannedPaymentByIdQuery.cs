using MediatR;
using TrackIt.Application.DTOs.PlannedPayments;

namespace TrackIt.Application.Features.PlannedPayments.Queries;

/// <summary>
///     Запрос на получение платежа по ID.
/// </summary>
public class GetPlannedPaymentByIdQuery : IRequest<DetailedPlannedPaymentDto?>
{
    /// <summary>
    ///     Идентификатор платежа
    /// </summary>
    public Guid PlannedPaymentId { get; set; }
}