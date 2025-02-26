namespace TrackIt.Application.DTOs.Categories;

/// <summary>
///     Детализированная модель категории
/// </summary>
public sealed class DetailedCategoryDto : CategoryDto
{
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///     Дата обновления
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}