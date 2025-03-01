using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs.Analytics;
using TrackIt.Application.Features.Analytics.Queries;

namespace TrackIt.API.Controllers;

/// <summary>
///     Контроллер аналитики финансов
/// </summary>
[Route("api/analytics")]
[ApiController]
public class AnalyticsController(IMediator mediator) : ControllerBase
{
	/// <summary>
	///     Получение общего баланса пользователя
	/// </summary>
	[HttpGet("balance")]
	[ProducesResponseType(typeof(BalanceDto), 200)]
	public async Task<IActionResult> GetBalance([FromQuery] GetBalanceQuery query, CancellationToken cancellationToken)
	{
		var result = await mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	/// <summary>
	///     Получение расходов по категориям
	/// </summary>
	[HttpGet("category-spending")]
	[ProducesResponseType(typeof(IEnumerable<CategorySpendingDto>), 200)]
	public async Task<IActionResult> GetCategorySpending([FromQuery] GetCategorySpendingQuery query, CancellationToken cancellationToken)
	{
		var result = await mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	/// <summary>
	///     Получение динамики расходов за месяц
	/// </summary>
	[HttpGet("monthly-trend")]
	[ProducesResponseType(typeof(IEnumerable<DailySpendingDto>), 200)]
	public async Task<IActionResult> GetDailySpending([FromQuery] GetDailySpendingDtoQuery query, CancellationToken cancellationToken)
	{
		var result = await mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	/// <summary>
	///     Получение среднего уровня месячных расходов
	/// </summary>
	[HttpGet("monthly-average")]
	[ProducesResponseType(typeof(MonthlyAverageDto), 200)]
	public async Task<IActionResult> GetMonthlyAverageSpending([FromQuery] GetMonthlyAverageDtoQuery query, CancellationToken cancellationToken)
	{
		var result = await mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	/// <summary>
	///     Получение топ-3 самых затратных категорий
	/// </summary>
	[HttpGet("top-categories")]
	[ProducesResponseType(typeof(IEnumerable<TopCategoryDto>), 200)]
	public async Task<IActionResult> GetTopExpensiveCategories([FromQuery] GetTopCategoryQuery query, CancellationToken cancellationToken)
	{
		var result = await mediator.Send(query, cancellationToken);
		return Ok(result);
	}
}
