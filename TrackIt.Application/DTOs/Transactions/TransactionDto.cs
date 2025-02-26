namespace TrackIt.Application.DTOs.Transactions;

/// <summary>
///     Модель транзакции
/// </summary>
public class TransactionDto
{
    /// <summary>
    ///     Уникальный идентификатор транзакции.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    ///     Уникальный идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Сумма транзакции.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     Дата транзакции.
    /// </summary>
    public DateTime Date { get; set; }
}