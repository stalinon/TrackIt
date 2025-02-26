using MediatR;
using TrackIt.Application.DTOs.Transactions;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Команда для создания новой транзакции.
/// </summary>
public class CreateTransactionCommand : IRequest<TransactionDto>
{
    /// <summary>
    ///     Идентификатор категории.
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