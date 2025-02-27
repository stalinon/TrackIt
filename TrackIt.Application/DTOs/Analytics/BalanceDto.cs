namespace TrackIt.Application.DTOs.Analytics;

/// <summary>
///     Модель общего баланса
/// </summary>
public class BalanceDto
{
    /// <summary>
    ///     Сумма всех доходов
    /// </summary>
    public decimal TotalIncome { get; set; }
    
    /// <summary>
    ///     Сумма всех расходов
    /// </summary>
    public decimal TotalExpense { get; set; }
    
    /// <summary>
    ///     Баланс
    /// </summary>
    public decimal Balance { get; set; }
}