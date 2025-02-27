using TrackIt.Application.DTOs.Budgets;
using TrackIt.Application.Features.Budgets.Commands;
using TrackIt.Application.Features.Budgets.Queries;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Сервис лимитов на бюджет
/// </summary>
public interface IBudgetService
{
    /// <summary>
    ///     Создать лимит
    /// </summary>
    Task<BudgetDto> CreateAsync(CreateBudgetCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить лимит
    /// </summary>
    Task<bool> DeleteAsync(DeleteBudgetCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить лимит
    /// </summary>
    Task<BudgetDto> UpdateAsync(UpdateBudgetCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить лимит
    /// </summary>
    Task<DetailedBudgetDto?> GetByIdAsync(GetBudgetByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список лимитов
    /// </summary>
    Task<IEnumerable<BudgetDto>> ListAsync(GetBudgetQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить лимит
    /// </summary>
    /// <returns></returns>
    Task CheckLimitAsync(Guid transactionId, CancellationToken cancellationToken);
}