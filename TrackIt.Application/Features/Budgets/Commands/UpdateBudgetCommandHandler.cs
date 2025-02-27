using MediatR;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Budgets.Commands;

/// <summary>
///     Обработчик команды <see cref="UpdateBudgetCommand"/>
/// </summary> 
internal sealed class UpdateBudgetCommandHandler(IBudgetService service) : IRequestHandler<UpdateBudgetCommand, BudgetDto>
{
    /// <inheritdoc />
    public async Task<BudgetDto> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        return await service.UpdateAsync(request, cancellationToken);
    }
}