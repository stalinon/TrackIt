using MediatR;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.PlannedPayments.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetPlannedPaymentQuery" />
/// </summary>
internal sealed class GetPlannedPaymentQueryHandler(IPlannedPaymentService service) : IRequestHandler<GetPlannedPaymentQuery, PagedList<PlannedPaymentDto>>
{
    /// <inheritdoc />
    public async Task<PagedList<PlannedPaymentDto>> Handle(GetPlannedPaymentQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}