using MediatR;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Budgets.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetBudgetQuery" />
/// </summary>
internal sealed class GetBudgetQueryHandler(IBudgetService service) : IRequestHandler<GetBudgetQuery, PagedList<BudgetDto>>
{
    /// <inheritdoc />
    public async Task<PagedList<BudgetDto>> Handle(GetBudgetQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}