using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Обработчик команды <see cref="CreateTransactionCommand"/>.
/// </summary>
internal sealed class CreateTransactionCommandHandler(ITransactionService service) : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    /// <inheritdoc />
    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        return await service.CreateAsync(request, cancellationToken);
    }
}