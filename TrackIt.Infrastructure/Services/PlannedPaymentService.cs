using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Application.Features.PlannedPayments.Commands;
using TrackIt.Application.Features.PlannedPayments.Queries;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IPlannedPaymentService" />
internal sealed class PlannedPaymentService(IUnitOfWork unitOfWork, IUserContext userContext) : IPlannedPaymentService
{
    /// <inheritdoc />
    public async Task<PlannedPaymentDto> CreateAsync(CreatePlannedPaymentCommand command, CancellationToken cancellationToken)
    {
        // Создание новой транзакции
        var transaction = new PlannedPaymentEntity
        {
            Id = Guid.NewGuid(),
            UserId = userContext.UserId,
            Amount = command.Amount,
            Description = command.Description,
            
        };

        // Добавляем в БД
        await unitOfWork.PlannedPayments.AddAsync(transaction);
        await unitOfWork.SaveChangesAsync();

        // Возвращаем DTO
        return new PlannedPaymentDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Date = transaction.Date
        };
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(DeletePlannedPaymentCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var transaction = await unitOfWork.PlannedPayments.GetByIdAsync(command.TransactionId);
        
        if (transaction == null || transaction.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Транзакция не найдена или доступ запрещен.");
        }

        unitOfWork.PlannedPayments.Remove(transaction);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<PlannedPaymentDto> UpdateAsync(UpdatePlannedPaymentCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var transaction = await unitOfWork.PlannedPayments.GetByIdAsync(command.TransactionId);
        
        if (transaction == null || transaction.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Транзакция не найдена или доступ запрещен.");
        }

        // Обновляем данные
        transaction.CategoryId = command.CategoryId;
        transaction.Amount = command.Amount;
        transaction.Description = command.Description;
        transaction.Date = command.Date;

        unitOfWork.PlannedPayments.Update(transaction);
        await unitOfWork.SaveChangesAsync();

        // Возвращаем обновленный объект
        return new PlannedPaymentDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Date = transaction.Date
        };
    }

    /// <inheritdoc />
    public async Task<DetailedPlannedPaymentDto?> GetByIdAsync(GetPlannedPaymentByIdQuery query, CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.PlannedPayments.GetByIdAsync(query.TransactionId);

        if (transaction == null || transaction.UserId != userContext.UserId)
        {
            return null;
        }

        return new DetailedPlannedPaymentDto
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
    public async Task<IEnumerable<PlannedPaymentDto>> ListAsync(GetPlannedPaymentQuery query, CancellationToken cancellationToken)
    {
        var transactions = await unitOfWork.PlannedPayments.GetPaginatedAsync(
            pageIndex: query.PageIndex,
            pageSize: query.Limit,
            filter: e => e.UserId == userContext.UserId,
            orderBy: e => e.DueDate);

        return transactions.Select(transaction => new PlannedPaymentDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Date = transaction.Date
        }).ToList();
    }
}