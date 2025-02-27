using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs.Categories;

namespace TrackIt.Application.Features.Categories.Queries;

/// <summary>
///     Запрос на получение категории по ID.
/// </summary>
public class GetCategoryByIdQuery : IRequest<DetailedCategoryDto?>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [FromQuery(Name = "category_id")]
    public Guid CategoryId { get; set; }
}