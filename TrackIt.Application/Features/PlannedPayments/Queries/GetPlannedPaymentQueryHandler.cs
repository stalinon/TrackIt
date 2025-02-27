using MediatR;
using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.PlannedPayments.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetPlannedPaymentQuery" />
/// </summary>
internal sealed class GetPlannedPaymentQueryHandler(IPlannedPaymentService service) : IRequestHandler<GetPlannedPaymentQuery, IEnumerable<PlannedPaymentDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<PlannedPaymentDto>> Handle(GetPlannedPaymentQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}