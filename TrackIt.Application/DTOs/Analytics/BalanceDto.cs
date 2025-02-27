using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs.Analytics;

/// <summary>
///     Модель общего баланса
/// </summary>
public class BalanceDto
{
    /// <summary>
    ///     Сумма всех доходов
    /// </summary>
    [JsonPropertyName("total_income")]
    public decimal TotalIncome { get; set; }
    
    /// <summary>
    ///     Сумма всех расходов
    /// </summary>
    [JsonPropertyName("total_expense")]
    public decimal TotalExpense { get; set; }
    
    /// <summary>
    ///     Баланс
    /// </summary>
    [JsonPropertyName("balance")]
    public decimal Balance { get; set; }
}