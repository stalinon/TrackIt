using MediatR;
using TrackIt.Application.DTOs.Analytics;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Получить траты по дням
/// </summary>
public class GetDailySpendingDtoQuery : IRequest<IEnumerable<DailySpendingDto>>;