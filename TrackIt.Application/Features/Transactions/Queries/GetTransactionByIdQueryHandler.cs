using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetTransactionByIdQuery"/>.
/// </summary>
public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
{
    private readonly ITransactionService _service;

    /// <inheritdoc cref="GetTransactionByIdQueryHandler" />
    public GetTransactionByIdQueryHandler(ITransactionService service)
    {
        _service = service;
    }

    /// <inheritdoc />
    public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request, cancellationToken);
    }
}