using MediatR;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Budgets.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetBudgetQuery" />
/// </summary>
internal sealed class GetBudgetQueryHandler(IBudgetService service) : IRequestHandler<GetBudgetQuery, IEnumerable<BudgetDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<BudgetDto>> Handle(GetBudgetQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}