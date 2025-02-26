using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Обработчик команды <see cref="UpdateTransactionCommand"/>.
/// </summary>
internal sealed class UpdateTransactionCommandHandler(ITransactionService service) : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    /// <inheritdoc />
    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        return await service.UpdateAsync(request, cancellationToken);
    }
}