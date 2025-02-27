using MediatR;
using TrackIt.Application.DTOs.Analytics;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetCategorySpendingQuery" />
/// </summary>
internal sealed class GetCategorySpendingQueryHandler(IFinanceAnalyticsService service) : IRequestHandler<GetCategorySpendingQuery, IEnumerable<CategorySpendingDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<CategorySpendingDto>> Handle(GetCategorySpendingQuery request, CancellationToken cancellationToken)
    {
        return await service.GetSpendingByCategoryAsync(cancellationToken);
    }
}