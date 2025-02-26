using MediatR;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Обработчик команды <see cref="DeleteTransactionCommand"/>.
/// </summary>
public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <inheritdoc cref="DeleteTransactionCommandHandler" />
    public DeleteTransactionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var transaction = await _unitOfWork.Transactions.GetByIdAsync(request.TransactionId);
        
        if (transaction == null || transaction.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("Транзакция не найдена или доступ запрещен.");
        }

        _unitOfWork.Transactions.Remove(transaction);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}