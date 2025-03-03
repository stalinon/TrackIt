using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Application.Features.Budgets.Commands;
using TrackIt.Application.Features.Budgets.Queries;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IBudgetService" />
internal sealed class BudgetService(IUnitOfWork unitOfWork, IUserContext userContext, ITelegramNotificationService notificationService) : IBudgetService
{
    /// <inheritdoc />
    public async Task<BudgetDto> CreateAsync(CreateBudgetCommand command, CancellationToken cancellationToken)
    {
        // Создание новой транзакции
        var entity = new BudgetEntity
        {
            Id = Guid.NewGuid(),
            UserId = userContext.UserId,
            CategoryId = command.CategoryId,
            LimitAmount = command.LimitAmount,
        };

        // Добавляем в БД
        await unitOfWork.Budgets.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();

        // Возвращаем DTO
        return new BudgetDto
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            LimitAmount = entity.LimitAmount,
        };
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(DeleteBudgetCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var entity = await unitOfWork.Budgets.GetByIdAsync(command.BudgetId);
        
        if (entity == null || entity.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Категория не найдена или доступ запрещен.");
        }

        unitOfWork.Budgets.Remove(entity);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<BudgetDto> UpdateAsync(UpdateBudgetCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var entity = await unitOfWork.Budgets.GetByIdAsync(command.BudgetId);
        if (entity == null || entity.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Категория не найдена или доступ запрещен.");
        }

        // Обновляем данные
        entity.CategoryId = command.CategoryId;
        entity.LimitAmount = command.LimitAmount;

        unitOfWork.Budgets.Update(entity);
        await unitOfWork.SaveChangesAsync();

        // Возвращаем обновленный объект
        return new BudgetDto
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            LimitAmount = entity.LimitAmount,
        };
    }

    /// <inheritdoc />
    public async Task<DetailedBudgetDto?> GetByIdAsync(GetBudgetByIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Budgets.GetByIdAsync(query.BudgetId);
        if (entity == null || entity.UserId != userContext.UserId)
        {
            return null;
        }

        return new DetailedBudgetDto
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            LimitAmount = entity.LimitAmount,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    /// <inheritdoc />
    public async Task<PagedList<BudgetDto>> ListAsync(GetBudgetQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<BudgetEntity, bool>> filter = e => e.UserId == userContext.UserId;
        var limits = await unitOfWork.Budgets.GetPaginatedAsync(
            pageIndex: query.PageIndex,
            pageSize: query.Limit,
            filter: filter,
            orderBy: e => e.UpdatedAt);
        
        var count = await unitOfWork.Budgets.CountAsync(filter);

        return new()
        {
            Total = count,
            Items = limits.Select(entity => new BudgetDto
            {
                Id = entity.Id,
                CategoryId = entity.CategoryId,
                LimitAmount = entity.LimitAmount,
            })
        };
    }

    /// <inheritdoc />
    public async Task CheckLimitAsync(Guid transactionId, CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.Transactions.GetByIdAsync(transactionId);
        var category = transaction!.Category;
        var limit = category.Budgets.FirstOrDefault(budget => budget.Id == transactionId);
        if (limit != null)
        {
            var from = transaction.Date.AddDays(-transaction.Date.Day);
            var to = transaction.Date;
            var amount = await unitOfWork.Transactions.GetQuery()
                .Where(e => e.UserId == userContext.UserId && e.Date < to && e.Date >= from)
                .SumAsync(e => e.Amount, cancellationToken: cancellationToken);

            if (amount > limit.LimitAmount)
            {
                var message = $"Превышен бюджет на {category.Name}. Бюджет: {limit.LimitAmount}";
                await notificationService.SendNotificationAsync(transaction.UserId, message, CancellationToken.None);
            }
        }
    }
}