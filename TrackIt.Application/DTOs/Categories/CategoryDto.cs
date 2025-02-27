using System.Text.Json.Serialization;
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
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     Название категории
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    ///     Тип категории
    /// </summary>
    [JsonPropertyName("type")]
    public CategoryType Type { get; set; }
    
    /// <summary>
    ///     Идентификатор пользователя
    /// </summary>
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }
}