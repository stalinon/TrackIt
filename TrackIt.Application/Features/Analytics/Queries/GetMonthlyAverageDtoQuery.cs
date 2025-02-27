using MediatR;
using TrackIt.Application.DTOs.Analytics;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Получить средние месячные траты
/// </summary>
public class GetMonthlyAverageDtoQuery : IRequest<MonthlyAverageDto>;