namespace TrackIt.Application.DTOs.Transactions;

/// <summary>
///     Детализированная модель транзакции
/// </summary>
public class DetailedTransactionDto : TransactionDto
{
    /// <summary>
    ///     Описание
    /// </summary>
    public string? Description { get; set; }
}