using System.Text.Json.Serialization;
using MediatR;
using TrackIt.Application.DTOs.Transactions;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Команда для обновления транзакции.
/// </summary>
public class UpdateTransactionCommand : IRequest<TransactionDto>
{
    /// <summary>
    ///     Уникальный идентификатор транзакции.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid TransactionId { get; set; }

    /// <summary>
    ///     Уникальный идентификатор категории.
    /// </summary>
    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; set; }

    /// <summary>
    ///     Сумма транзакции.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    ///     Описание транзакции.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    /// <summary>
    ///     Дата транзакции.
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
}