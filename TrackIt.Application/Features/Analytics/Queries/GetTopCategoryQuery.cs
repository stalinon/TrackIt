using MediatR;
using TrackIt.Application.DTOs.Analytics;

namespace TrackIt.Application.Features.Analytics.Queries;

/// <summary>
///     Получить топ категорий по тратам
/// </summary>
public class GetTopCategoryQuery : IRequest<IEnumerable<TopCategoryDto>>;