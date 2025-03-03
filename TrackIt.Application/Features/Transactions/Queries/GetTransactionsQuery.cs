using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Domain.Common;

namespace TrackIt.Application.Features.Transactions.Queries;

/// <summary>
///     Запрос на получение всех транзакций пользователя.
/// </summary>
public class GetTransactionsQuery : PagedQuery, IRequest<PagedList<TransactionDto>>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [FromQuery(Name = "category_id")]
    public Guid? CategoryId { get; set; } = null;
}