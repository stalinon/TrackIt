using MediatR;
using TrackIt.Application.DTOs.Analytics;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Получить траты по категориям
/// </summary>
public class GetCategorySpendingQuery : IRequest<IEnumerable<CategorySpendingDto>>;