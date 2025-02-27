using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Transactions;

/// <summary>
///     Детализированная модель транзакции
/// </summary>
public sealed class DetailedTransactionDto : TransactionDto
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
    
    /// <summary>
    ///     Описание
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [JsonPropertyName("category_id")]
    public Guid? CategoryId { get; set; }
}