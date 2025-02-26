using MediatR;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Domain.Enums;

namespace TrackIt.Application.Features.Categories.Commands;

/// <summary>
///     Запрос на создание категории
/// </summary>
public class CreateCategoryCommand : IRequest<CategoryDto>
{
    /// <summary>
    ///     Название категории
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    ///     Тип категории
    /// </summary>
    public CategoryType Type { get; set; }
}