using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetTransactionsQuery"/>.
/// </summary>
internal sealed class GetTransactionsQueryHandler(ITransactionService service) : IRequestHandler<GetTransactionsQuery, IEnumerable<TransactionDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}