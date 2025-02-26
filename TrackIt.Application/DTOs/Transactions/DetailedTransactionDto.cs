namespace TrackIt.Application.DTOs.Transactions;

/// <summary>
///     Детализированная модель транзакции
/// </summary>
public sealed class DetailedTransactionDto : TransactionDto
{
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///     Дата обновления
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    ///     Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; set; }
}