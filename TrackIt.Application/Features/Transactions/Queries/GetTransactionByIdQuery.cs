using MediatR;
using TrackIt.Application.DTOs.Transactions;

namespace TrackIt.Application.Features.Transactions.Queries;

/// <summary>
///     Запрос на получение транзакции по ID.
/// </summary>
public class GetTransactionByIdQuery : IRequest<DetailedTransactionDto?>
{
    /// <summary>
    ///     Уникальный идентификатор транзакции.
    /// </summary>
    public Guid TransactionId { get; set; }
}