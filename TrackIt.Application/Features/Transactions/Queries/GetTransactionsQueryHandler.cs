using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetTransactionsQuery"/>.
/// </summary>
public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, IEnumerable<DetailedTransactionDto>>
{
    private readonly ITransactionService _service;

    /// <inheritdoc cref="GetTransactionsQueryHandler" />
    public GetTransactionsQueryHandler(ITransactionService service)
    {
        _service = service;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<DetailedTransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _service.ListAsync(request, cancellationToken);
    }
}