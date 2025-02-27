using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Categories;

/// <summary>
///     Детализированная модель категории
/// </summary>
public sealed class DetailedCategoryDto : CategoryDto
{
    /// <summary>
    ///     Дата создания
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///     Дата обновления
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}