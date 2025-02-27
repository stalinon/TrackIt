using MediatR;
using TrackIt.Application.DTOs.Analytics;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetBalanceQuery" />
/// </summary>
internal sealed class GetBalanceQueryHandler(IFinanceAnalyticsService service) : IRequestHandler<GetBalanceQuery, BalanceDto>
{
    /// <inheritdoc />
    public async Task<BalanceDto> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        return await service.GetBalanceAsync(cancellationToken);
    }
}