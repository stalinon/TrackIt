using MediatR;
using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.PlannedPayments.Commands;

/// <summary>
///     Обработчик команды <see cref="CreatePlannedPaymentCommand" />
/// </summary>
internal sealed class CreatePlannedPaymentCommandHandler(IPlannedPaymentService service) : IRequestHandler<CreatePlannedPaymentCommand, PlannedPaymentDto>
{
    /// <inheritdoc />
    public async Task<PlannedPaymentDto> Handle(CreatePlannedPaymentCommand request, CancellationToken cancellationToken)
    {
        return await service.CreateAsync(request, cancellationToken);
    }
}