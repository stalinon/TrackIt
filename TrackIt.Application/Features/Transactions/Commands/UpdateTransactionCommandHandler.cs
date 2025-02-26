using MediatR;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Transactions.Commands;

/// <summary>
///     Обработчик команды <see cref="UpdateTransactionCommand"/>.
/// </summary>
public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <inheritdoc cref="UpdateTransactionCommandHandler" />
    public UpdateTransactionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var transaction = await _unitOfWork.Transactions.GetByIdAsync(request.TransactionId);
        
        if (transaction == null || transaction.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("Транзакция не найдена или доступ запрещен.");
        }

        // Обновляем данные
        transaction.CategoryId = request.CategoryId;
        transaction.Amount = request.Amount;
        transaction.Description = request.Description;
        transaction.Date = request.Date;

        _unitOfWork.Transactions.Update(transaction);
        await _unitOfWork.SaveChangesAsync();

        // Возвращаем обновленный объект
        return new TransactionDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Date = transaction.Date
        };
    }
}