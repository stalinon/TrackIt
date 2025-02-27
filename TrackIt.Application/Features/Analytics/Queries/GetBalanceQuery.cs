using MediatR;
using TrackIt.Application.DTOs.Analytics;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Запрос на получение общего баланса.
/// </summary>
public class GetBalanceQuery : IRequest<BalanceDto>;