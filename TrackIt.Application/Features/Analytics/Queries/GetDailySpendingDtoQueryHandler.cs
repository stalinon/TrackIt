using MediatR;
using TrackIt.Application.DTOs.Analytics;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetDailySpendingDtoQuery" />
/// </summary>
internal sealed class GetDailySpendingDtoQueryHandler(IFinanceAnalyticsService service) : IRequestHandler<GetDailySpendingDtoQuery, IEnumerable<DailySpendingDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<DailySpendingDto>> Handle(GetDailySpendingDtoQuery request, CancellationToken cancellationToken)
    {
        return await service.GetMonthlySpendingTrendAsync(DateTime.Today.Year, DateTime.Today.Month, cancellationToken);
    }
}