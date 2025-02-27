using Microsoft.EntityFrameworkCore;
using TrackIt.Application.DTOs.Analytics;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Enums;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IFinanceAnalyticsService" />
internal sealed class FinanceAnalyticsService(
	IUnitOfWork unitOfWork, 
	IUserContext userContext) : IFinanceAnalyticsService
{
	/// <inheritdoc />
	public async Task<BalanceDto> GetBalanceAsync(CancellationToken cancellationToken)
	{
		// Вычисляем баланс на стороне БД
		var result = await unitOfWork.Transactions
			.GetQuery()
			.Where(t => t.UserId == userContext.UserId)
			.GroupBy(t => t.Category.Type)
			.Select(g => new
			{
				Type = g.Key,
				TotalAmount = g.Sum(t => t.Amount)
			})
			.ToListAsync(cancellationToken);

		// Разделяем суммы доходов и расходов
		var totalIncome = result.Where(r => r.Type == CategoryType.INCOME).Sum(r => r.TotalAmount);
		var totalExpense = result.Where(r => r.Type == CategoryType.EXPENSE).Sum(r => r.TotalAmount);

		// Возвращаем DTO
		return new BalanceDto
		{
			TotalIncome = totalIncome,
			TotalExpense = totalExpense,
			Balance = totalIncome - totalExpense
		};
	}

	/// <inheritdoc />
	public async Task<IEnumerable<CategorySpendingDto>> GetSpendingByCategoryAsync(CancellationToken cancellationToken)
	{
		// Группируем расходы по категориям на стороне БД
		var result = await unitOfWork.Transactions
			.GetQuery()
			.Where(t => t.UserId == userContext.UserId && t.Category.Type == CategoryType.EXPENSE)
			.GroupBy(t => t.Category.Name)
			.Select(g => new CategorySpendingDto
			{
				CategoryName = g.Key,
				TotalSpent = g.Sum(t => t.Amount)
			})
			.OrderByDescending(c => c.TotalSpent)
			.ToListAsync(cancellationToken);

		return result;
	}

	/// <inheritdoc />
	public async Task<IEnumerable<DailySpendingDto>> GetMonthlySpendingTrendAsync(int year, int month, CancellationToken cancellationToken)
	{
		// Группируем расходы по дням месяца на стороне БД
		var result = await unitOfWork.Transactions
			.GetQuery()
			.Where(t => t.UserId == userContext.UserId && t.Date.Year == year && t.Date.Month == month)
			.GroupBy(t => t.Date.Day)
			.Select(g => new DailySpendingDto
			{
				Day = g.Key,
				TotalSpent = g.Sum(t => t.Amount)
			})
			.OrderBy(d => d.Day)
			.ToListAsync(cancellationToken);

		return result;
	}

	/// <inheritdoc />
	public async Task<MonthlyAverageDto> GetMonthlyAverageSpendingAsync(CancellationToken cancellationToken)
	{
		// Группируем расходы по месяцам на стороне БД
		var result = await unitOfWork.Transactions
			.GetQuery()
			.Where(t => t.UserId == userContext.UserId && t.Category.Type == CategoryType.EXPENSE)
			.GroupBy(t => new { t.Date.Year, t.Date.Month })
			.Select(g => g.Sum(t => t.Amount))
			.ToListAsync(cancellationToken);

		// Вычисляем среднее значение
		var average = result.Count == 0 ? 0 : result.Average();

		return new MonthlyAverageDto
		{
			AverageMonthlySpending = average
		};
	}

	/// <inheritdoc />
	public async Task<IEnumerable<TopCategoryDto>> GetTopExpensiveCategoriesAsync(CancellationToken cancellationToken)
	{
		// Группируем и берем топ-3 самых затратных категорий на стороне БД
		var result = await unitOfWork.Transactions
			.GetQuery()
			.Where(t => t.UserId == userContext.UserId && t.Category.Type == CategoryType.EXPENSE)
			.GroupBy(t => t.Category.Name)
			.Select(g => new TopCategoryDto
			{
				CategoryName = g.Key,
				TotalSpent = g.Sum(t => t.Amount)
			})
			.OrderByDescending(c => c.TotalSpent)
			.Take(3)
			.ToListAsync(cancellationToken);

		return result;
	}
}