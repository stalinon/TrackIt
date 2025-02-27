using MediatR;
using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.PlannedPayments.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetPlannedPaymentByIdQuery" />
/// </summary>
internal sealed class GetPlannedPaymentByIdQueryHandler(IPlannedPaymentService service) : IRequestHandler<GetPlannedPaymentByIdQuery, DetailedPlannedPaymentDto?>
{
    /// <inheritdoc />
    public async Task<DetailedPlannedPaymentDto?> Handle(GetPlannedPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetByIdAsync(request, cancellationToken);
    }
}