using MediatR;
using TrackIt.Application.DTOs.PlannedPayments;

namespace TrackIt.Application.Features.PlannedPayments.Commands;

/// <summary>
///     Запрос на создание запланированного платежа
/// </summary>
public class CreatePlannedPaymentCommand : IRequest<PlannedPaymentDto>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    ///     Объем оплаты
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    ///     Дата
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    ///     Описание
    /// </summary>
    public string Description { get; set; } = default!;
}