using System.Text.Json.Serialization;
using MediatR;
using TrackIt.Application.DTOs.Budgets;

namespace TrackIt.Application.Features.Budgets.Commands;

/// <summary>
///     Запрос на обновление запланированного платежа
/// </summary>
public class UpdateBudgetCommand : IRequest<BudgetDto>
{
    /// <summary>
    ///     Идентификатор запланированного платежа
    /// </summary>
    [JsonPropertyName("id")]
    public Guid BudgetId { get; set; }
    
    /// <summary>
    ///     Величина лимита
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal LimitAmount { get; set; }
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [JsonPropertyName("category_id")]
    public Guid? CategoryId { get; set; }
}