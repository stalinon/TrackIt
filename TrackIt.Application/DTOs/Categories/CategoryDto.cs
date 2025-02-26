using TrackIt.Domain.Enums;

namespace TrackIt.Application.DTOs.Categories;

/// <summary>
///     Модель категории
/// </summary>
public class CategoryDto
{
    /// <summary>
    ///     Уникальный идентификатор категории
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Название категории
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    ///     Тип категории
    /// </summary>
    public CategoryType Type { get; set; }
    
    /// <summary>
    ///     Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}