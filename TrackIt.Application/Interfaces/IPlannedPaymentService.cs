using TrackIt.Application.DTOs.PlannedPayments;
using TrackIt.Application.Features.PlannedPayments.Commands;
using TrackIt.Application.Features.PlannedPayments.Queries;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Сервис запланированных оплат
/// </summary>
public interface IPlannedPaymentService
{
    /// <summary>
    ///     Создать запланированyю оплату
    /// </summary>
    Task<PlannedPaymentDto> CreateAsync(CreatePlannedPaymentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить запланированyю оплату
    /// </summary>
    Task<bool> DeleteAsync(DeletePlannedPaymentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить запланированyю оплату
    /// </summary>
    Task<PlannedPaymentDto> UpdateAsync(UpdatePlannedPaymentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить запланированyю оплату
    /// </summary>
    Task<DetailedPlannedPaymentDto?> GetByIdAsync(GetPlannedPaymentByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список запланированных оплат
    /// </summary>
    Task<IEnumerable<PlannedPaymentDto>> ListAsync(GetPlannedPaymentQuery query, CancellationToken cancellationToken);
}