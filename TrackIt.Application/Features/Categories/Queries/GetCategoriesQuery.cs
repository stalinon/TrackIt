using MediatR;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Domain.Common;

namespace TrackIt.Application.Features.Categories.Queries;

/// <summary>
///     Запрос на получение категорий.
/// </summary>
public class GetCategoriesQuery : PagedQuery, IRequest<IEnumerable<CategoryDto>>;