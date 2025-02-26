using MediatR;
using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.PlannedPayments.Commands;

/// <summary>
///     Обработчик команды <see cref="UpdatePlannedPaymentCommand"/>
/// </summary> 
internal sealed class UpdatePlannedPaymentCommandHandler(IPlannedPaymentService service) : IRequestHandler<UpdatePlannedPaymentCommand, PlannedPaymentDto>
{
    /// <inheritdoc />
    public async Task<PlannedPaymentDto> Handle(UpdatePlannedPaymentCommand request, CancellationToken cancellationToken)
    {
        return await service.UpdateAsync(request, cancellationToken);
    }
}