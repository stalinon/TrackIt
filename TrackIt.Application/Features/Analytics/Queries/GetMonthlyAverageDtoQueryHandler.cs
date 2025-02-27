using MediatR;
using TrackIt.Application.DTOs.Analytics;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetMonthlyAverageDtoQuery" />
/// </summary>
internal sealed class GetMonthlyAverageDtoQueryHandler(IFinanceAnalyticsService service) : IRequestHandler<GetMonthlyAverageDtoQuery, MonthlyAverageDto>
{
    /// <inheritdoc />
    public async Task<MonthlyAverageDto> Handle(GetMonthlyAverageDtoQuery request, CancellationToken cancellationToken)
    {
        return await service.GetMonthlyAverageSpendingAsync(cancellationToken);
    }
}