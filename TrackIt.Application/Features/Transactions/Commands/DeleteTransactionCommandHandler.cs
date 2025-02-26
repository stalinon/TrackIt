using MediatR;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Обработчик команды <see cref="DeleteTransactionCommand"/>.
/// </summary>
public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly ITransactionService _service;

    /// <inheritdoc cref="DeleteTransactionCommandHandler" />
    public DeleteTransactionCommandHandler(ITransactionService service)
    {
        _service = service;
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request, cancellationToken);
    }
}