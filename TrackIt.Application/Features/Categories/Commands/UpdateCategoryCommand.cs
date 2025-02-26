using MediatR;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Domain.Enums;

namespace TrackIt.Application.Features.Categories.Commands;

/// <summary>
///     Запрос на обновление категории
/// </summary>
public class UpdateCategoryCommand : IRequest<CategoryDto>
{
    /// <summary>
    ///     Уникальный идентификатор категории
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    ///     Название категории
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    ///     Тип категории
    /// </summary>
    public CategoryType Type { get; set; }
}