using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs;

/// <summary>
///     Пагинированный список
/// </summary>
public class PagedList<T>
{
    /// <summary>
    ///     Элементы
    /// </summary>
    [JsonPropertyName("items")]
    public IEnumerable<T> Items { get; set; }

    /// <summary>
    ///     Всего элементов
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }
}