using TrackIt.Application.DTOs.Analytics;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Интерфейс сервиса аналитики финансовых операций
/// </summary>
public interface IFinanceAnalyticsService
{
    /// <summary>
    ///     Получить общий баланс пользователя
    /// </summary>
    Task<BalanceDto> GetBalanceAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Группировка расходов по категориям
    /// </summary>
    Task<IEnumerable<CategorySpendingDto>> GetSpendingByCategoryAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Динамика расходов за месяц
    /// </summary>
    Task<IEnumerable<DailySpendingDto>> GetMonthlySpendingTrendAsync(int year, int month, CancellationToken cancellationToken);

    /// <summary>
    ///     Прогнозирование средних расходов в месяц
    /// </summary>
    Task<MonthlyAverageDto> GetMonthlyAverageSpendingAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Самые затратные категории (топ-3)
    /// </summary>
    Task<IEnumerable<TopCategoryDto>> GetTopExpensiveCategoriesAsync(CancellationToken cancellationToken);
}
