using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Обработчик команды <see cref="UpdateTransactionCommand"/>.
/// </summary>
public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    private readonly ITransactionService _service;

    /// <inheritdoc cref="UpdateTransactionCommandHandler" />
    public UpdateTransactionCommandHandler(ITransactionService service)
    {
        _service = service;
    }

    /// <inheritdoc />
    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(request, cancellationToken);
    }
}