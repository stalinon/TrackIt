using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Domain.Common;

namespace TrackIt.Application.Features.Transactions.Queries;

/// <summary>
///     Запрос на получение всех транзакций пользователя.
/// </summary>
public class GetTransactionsQuery : PagedQuery, IRequest<IEnumerable<TransactionDto>>
{
    /// <summary>
    ///     Уникальный идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
}