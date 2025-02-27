using MediatR;
using TrackIt.Application.Features.Categories.Commands;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.PlannedPayments.Commands;

/// <summary>
///     Обработчик команды <see cref="DeletePlannedPaymentCommand"/>
/// </summary>
internal sealed class DeletePlannedPaymentCommandHandler(IPlannedPaymentService service) : IRequestHandler<DeletePlannedPaymentCommand, bool>
{
    /// <inheritdoc />
    public async Task<bool> Handle(DeletePlannedPaymentCommand request, CancellationToken cancellationToken)
    {
        return await service.DeleteAsync(request, cancellationToken);
    }
}