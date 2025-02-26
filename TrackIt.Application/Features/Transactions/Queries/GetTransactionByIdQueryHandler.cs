using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetTransactionByIdQuery"/>.
/// </summary>
internal sealed class GetTransactionByIdQueryHandler(ITransactionService service) : IRequestHandler<GetTransactionByIdQuery, DetailedTransactionDto?>
{
    /// <inheritdoc />
    public async Task<DetailedTransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetByIdAsync(request, cancellationToken);
    }
}