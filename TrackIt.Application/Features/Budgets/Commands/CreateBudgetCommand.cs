using System.Text.Json.Serialization;
using MediatR;
using TrackIt.Application.DTOs.Budgets;

namespace TrackIt.Application.Features.Budgets.Commands;

/// <summary>
///     Запрос на создание запланированного платежа
/// </summary>
public class CreateBudgetCommand : IRequest<BudgetDto>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; set; }
    
    /// <summary>
    ///     Величина лимита
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal LimitAmount { get; set; }
}