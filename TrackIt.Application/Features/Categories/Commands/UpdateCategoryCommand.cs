using System.Text.Json.Serialization;
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
    [JsonPropertyName("id")]
    public Guid CategoryId { get; set; }

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
}