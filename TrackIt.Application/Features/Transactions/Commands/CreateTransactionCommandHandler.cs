using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Обработчик команды <see cref="CreateTransactionCommand"/>.
/// </summary>
public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ITransactionService _service;

    /// <inheritdoc cref="CreateTransactionCommandHandler" />
    public CreateTransactionCommandHandler(ITransactionService service)
    {
        _service = service;
    }

    /// <inheritdoc />
    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request, cancellationToken);
    }
}