using MediatR;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Budgets.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetBudgetByIdQuery" />
/// </summary>
internal sealed class GetBudgetByIdQueryHandler(IBudgetService service) : IRequestHandler<GetBudgetByIdQuery, DetailedBudgetDto?>
{
    /// <inheritdoc />
    public async Task<DetailedBudgetDto?> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetByIdAsync(request, cancellationToken);
    }
}