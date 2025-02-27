using System.Text.Json.Serialization;
using MediatR;

namespace TrackIt.Application.Features.PlannedPayments.Commands;

/// <summary>
///     Запрос на удаление запланированного платежа
/// </summary>
public class DeletePlannedPaymentCommand : IRequest<bool>
{
    /// <summary>
    ///     Идентификатор запланированного платежа
    /// </summary>
    [JsonPropertyName("id")]
    public Guid PlannedPaymentId { get; set; }
}