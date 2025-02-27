using MediatR;
using TrackIt.Application.Features.Categories.Commands;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Budgets.Commands;

/// <summary>
///     Обработчик команды <see cref="DeleteBudgetCommand"/>
/// </summary>
internal sealed class DeleteBudgetCommandHandler(IBudgetService service) : IRequestHandler<DeleteBudgetCommand, bool>
{
    /// <inheritdoc />
    public async Task<bool> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        return await service.DeleteAsync(request, cancellationToken);
    }
}