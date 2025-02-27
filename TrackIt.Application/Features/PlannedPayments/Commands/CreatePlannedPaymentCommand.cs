using System.Text.Json.Serialization;
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
    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; set; }
    
    /// <summary>
    ///     Объем оплаты
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    
    /// <summary>
    ///     Дата
    /// </summary>
    [JsonPropertyName("due_date")]
    public DateTime DueDate { get; set; }

    /// <summary>
    ///     Описание
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
}