using MediatR;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Обработчик команды <see cref="DeleteTransactionCommand"/>.
/// </summary>
internal sealed class DeleteTransactionCommandHandler(ITransactionService service) : IRequestHandler<DeleteTransactionCommand, bool>
{
    /// <inheritdoc />
    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        return await service.DeleteAsync(request, cancellationToken);
    }
}