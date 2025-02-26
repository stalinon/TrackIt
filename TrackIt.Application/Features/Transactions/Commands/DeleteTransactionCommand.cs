using MediatR;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Команда для удаления транзакции.
/// </summary>
public class DeleteTransactionCommand : IRequest<bool>
{
    /// <summary>
    ///     Уникальный идентификатор транзакции.
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    ///     Уникальный идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
}