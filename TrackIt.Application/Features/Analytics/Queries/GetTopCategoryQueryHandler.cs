using MediatR;
using TrackIt.Application.DTOs.Analytics;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetTopCategoryQuery" />
/// </summary>
internal sealed class GetTopCategoryQueryHandler(IFinanceAnalyticsService service) : IRequestHandler<GetTopCategoryQuery, IEnumerable<TopCategoryDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<TopCategoryDto>> Handle(GetTopCategoryQuery request, CancellationToken cancellationToken)
    {
        return await service.GetTopExpensiveCategoriesAsync(cancellationToken);
    }
}