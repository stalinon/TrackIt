using System.Text.Json.Serialization;
using MediatR;

namespace TrackIt.Application.Features.Categories.Commands;

/// <summary>
///     Запрос на удаление категории
/// </summary>
public class DeleteCategoryCommand : IRequest<bool>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [JsonPropertyName("id")]
    public Guid CategoryId { get; set; }
}