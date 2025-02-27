using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Features.Transactions.Commands;
using TrackIt.Application.Features.Transactions.Queries;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="ITransactionService" />
internal sealed class TransactionService(IUnitOfWork unitOfWork, IUserContext userContext, IBudgetService budgetService) : ITransactionService
{
    /// <inheritdoc />
    public async Task<TransactionDto> CreateAsync(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        // Создание новой транзакции
        var transaction = new TransactionEntity
        {
            Id = Guid.NewGuid(),
            UserId = userContext.UserId,
            CategoryId = command.CategoryId,
            Amount = command.Amount,
            Description = command.Description,
            Date = command.Date
        };

        // Добавляем в БД
        await unitOfWork.Transactions.AddAsync(transaction);
        await unitOfWork.SaveChangesAsync();
        
        await budgetService.CheckLimitAsync(transaction.Id, cancellationToken);

        // Возвращаем DTO
        return new TransactionDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Date = transaction.Date
        };
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(DeleteTransactionCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var transaction = await unitOfWork.Transactions.GetByIdAsync(command.TransactionId);
        
        if (transaction == null || transaction.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Транзакция не найдена или доступ запрещен.");
        }

        unitOfWork.Transactions.Remove(transaction);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<TransactionDto> UpdateAsync(UpdateTransactionCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var transaction = await unitOfWork.Transactions.GetByIdAsync(command.TransactionId);
        
        if (transaction == null || transaction.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Транзакция не найдена или доступ запрещен.");
        }

        // Обновляем данные
        transaction.CategoryId = command.CategoryId;
        transaction.Amount = command.Amount;
        transaction.Description = command.Description;
        transaction.Date = command.Date;

        unitOfWork.Transactions.Update(transaction);
        await unitOfWork.SaveChangesAsync();
        
        await budgetService.CheckLimitAsync(transaction.Id, cancellationToken);

        // Возвращаем обновленный объект
        return new TransactionDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Date = transaction.Date
        };
    }

    /// <inheritdoc />
    public async Task<DetailedTransactionDto?> GetByIdAsync(GetTransactionByIdQuery query, CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.Transactions.GetByIdAsync(query.TransactionId);

        if (transaction == null || transaction.UserId != userContext.UserId)
        {
            return null;
        }

        return new DetailedTransactionDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description,
            CategoryId = transaction.CategoryId,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt
        };
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TransactionDto>> ListAsync(GetTransactionsQuery query, CancellationToken cancellationToken)
    {
        var transactions = await unitOfWork.Transactions.GetPaginatedAsync(
            pageIndex: query.PageIndex,
            pageSize: query.Limit,
            filter: e => e.UserId == userContext.UserId && (query.CategoryId == null || query.CategoryId == e.CategoryId),
            orderBy: e => e.Date);

        return transactions.Select(transaction => new TransactionDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Date = transaction.Date
        }).ToList();
    }
}