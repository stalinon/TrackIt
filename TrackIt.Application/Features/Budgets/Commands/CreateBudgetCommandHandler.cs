using MediatR;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Budgets.Commands;

/// <summary>
///     Обработчик команды <see cref="CreateBudgetCommand" />
/// </summary>
internal sealed class CreateBudgetCommandHandler(IBudgetService service) : IRequestHandler<CreateBudgetCommand, BudgetDto>
{
    /// <inheritdoc />
    public async Task<BudgetDto> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        return await service.CreateAsync(request, cancellationToken);
    }
}