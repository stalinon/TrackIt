namespace TrackIt.Application.DTOs.PlannedPayments;

/// <summary>
///     Модель запланированной платы
/// </summary>
public class PlannedPaymentDto
{
    /// <summary>
    ///     Объем оплаты
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    ///     Дата
    /// </summary>
    public DateTime DueDate { get; set; }
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; set; }
}