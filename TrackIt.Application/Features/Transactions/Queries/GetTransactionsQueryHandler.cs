using MediatR;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetTransactionsQuery"/>.
/// </summary>
internal sealed class GetTransactionsQueryHandler(ITransactionService service) : IRequestHandler<GetTransactionsQuery, PagedList<TransactionDto>>
{
    /// <inheritdoc />
    public async Task<PagedList<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}