using Hangfire;
using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Application.Features.PlannedPayments.Commands;
using TrackIt.Application.Features.PlannedPayments.Queries;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IPlannedPaymentService" />
internal sealed class PlannedPaymentService(IUnitOfWork unitOfWork, IUserContext userContext, ITelegramNotificationService notificationService) : IPlannedPaymentService
{
    /// <summary>
    ///     Стандартное время перед оплатой для напоминания
    /// </summary>
    private const int DaysBeforePayment = 3;
    
    /// <inheritdoc />
    public async Task<PlannedPaymentDto> CreateAsync(CreatePlannedPaymentCommand command, CancellationToken cancellationToken)
    {
        // Создание новой транзакции
        var payment = new PlannedPaymentEntity
        {
            Id = Guid.NewGuid(),
            UserId = userContext.UserId,
            Amount = command.Amount,
            Description = command.Description,
            CategoryId = command.CategoryId,
        };

        // Добавляем в БД
        await unitOfWork.PlannedPayments.AddAsync(payment);
        await unitOfWork.SaveChangesAsync();
        
        await ScheduleReminderAsync(payment.Id, DaysBeforePayment, cancellationToken);

        // Возвращаем DTO
        return new PlannedPaymentDto
        {
            Amount = payment.Amount,
            DueDate = payment.DueDate,
            CategoryId = command.CategoryId,
        };
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(DeletePlannedPaymentCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var payment = await unitOfWork.PlannedPayments.GetByIdAsync(command.PlannedPaymentId);
        if (payment == null || payment.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Транзакция не найдена или доступ запрещен.");
        }
        
        await DeleteReminderAsync(payment.Id, cancellationToken);

        unitOfWork.PlannedPayments.Remove(payment);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<PlannedPaymentDto> UpdateAsync(UpdatePlannedPaymentCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var payment = await unitOfWork.PlannedPayments.GetByIdAsync(command.PlannedPaymentId);
        
        if (payment == null || payment.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Транзакция не найдена или доступ запрещен.");
        }

        // Обновляем данные
        payment.Amount = command.Amount;
        payment.Description = command.Description;
        payment.DueDate = command.DueDate;
        payment.CategoryId = command.CategoryId;

        unitOfWork.PlannedPayments.Update(payment);
        await unitOfWork.SaveChangesAsync();
        
        await ScheduleReminderAsync(payment.Id, DaysBeforePayment, cancellationToken);

        // Возвращаем обновленный объект
        return new PlannedPaymentDto
        {
            Amount = payment.Amount,
            DueDate = payment.DueDate,
            CategoryId = command.CategoryId,
        };
    }

    /// <inheritdoc />
    public async Task<DetailedPlannedPaymentDto?> GetByIdAsync(GetPlannedPaymentByIdQuery query, CancellationToken cancellationToken)
    {
        var payment = await unitOfWork.PlannedPayments.GetByIdAsync(query.PlannedPaymentId);

        if (payment == null || payment.UserId != userContext.UserId)
        {
            return null;
        }

        return new DetailedPlannedPaymentDto
        {
            Amount = payment.Amount,
            DueDate = payment.DueDate,
            Description = payment.Description,
            CreatedAt = payment.CreatedAt,
            UpdatedAt = payment.UpdatedAt,
            CategoryId = payment.CategoryId,
        };
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PlannedPaymentDto>> ListAsync(GetPlannedPaymentQuery query, CancellationToken cancellationToken)
    {
        var payments = await unitOfWork.PlannedPayments.GetPaginatedAsync(
            pageIndex: query.PageIndex,
            pageSize: query.Limit,
            filter: e => e.UserId == userContext.UserId && (query.CategoryId == null || e.CategoryId == query.CategoryId),
            orderBy: e => e.DueDate);

        return payments.Select(payment => new PlannedPaymentDto
        {
            DueDate = payment.DueDate,
            Amount = payment.Amount,
            CategoryId = payment.CategoryId,
        }).ToList();
    }

    private async Task DeleteReminderAsync(Guid paymentId, CancellationToken cancellationToken)
    {
        var payment = await unitOfWork.PlannedPayments.GetByIdAsync(paymentId);
        if (payment == null)
        {
            throw new Exception($"Запланированный платеж {paymentId} не найден");
        }

        if (payment.ScheduleId != null)
        {
            BackgroundJob.Delete(payment.ScheduleId);
        }
    }

    private async Task ScheduleReminderAsync(Guid paymentId, int daysBefore, CancellationToken cancellationToken)
    {
        var payment = await unitOfWork.PlannedPayments.GetByIdAsync(paymentId);
        if (payment == null)
        {
            throw new Exception($"Запланированный платеж {paymentId} не найден");
        }

        var reminderDate = payment.DueDate.AddDays(-daysBefore);
        if (reminderDate <= DateTime.UtcNow)
        {
            throw new Exception("Дата напоминания уже прошла");
        }

        if (payment.ScheduleId == null)
        {
            var id = BackgroundJob.Schedule(() =>
                    SendReminder(payment.UserId, payment.Amount, payment.Category.Name, payment.DueDate),
                reminderDate);
            payment.ScheduleId = id;
        }
        else
        {
            BackgroundJob.Reschedule(payment.ScheduleId, reminderDate);
        }

        await unitOfWork.SaveChangesAsync();
    }

    private async Task SendReminder(Guid userId, decimal amount, string? category, DateTime dueDate)
    {
        var message = $"Напоминание: {dueDate:dd.MM.yyyy} запланирован платеж {amount} {category}.";
        await notificationService.SendNotificationAsync(userId, message, CancellationToken.None);
    }
}