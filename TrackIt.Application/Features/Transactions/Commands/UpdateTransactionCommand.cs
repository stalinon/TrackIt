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
    public Guid TransactionId { get; set; }

    /// <summary>
    ///     Уникальный идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    ///     Сумма транзакции.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     Описание транзакции.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    ///     Дата транзакции.
    /// </summary>
    public DateTime Date { get; set; }
}