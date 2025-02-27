using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Transactions;

/// <summary>
///     Модель транзакции
/// </summary>
public class TransactionDto
{
    /// <summary>
    ///     Уникальный идентификатор транзакции.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    ///     Уникальный идентификатор пользователя.
    /// </summary>
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }

    /// <summary>
    ///     Сумма транзакции.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    ///     Дата транзакции.
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
}