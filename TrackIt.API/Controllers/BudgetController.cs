using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs.Budgets;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Features.Budgets.Commands;
using TrackIt.Application.Features.Budgets.Queries;

namespace TrackIt.API.Controllers;

/// <summary>
///     Контроллер лимитов бюджета
/// </summary>
[Route("api/budgets")]
[ApiController]
[Authorize]
public class BudgetController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Создание лимита
    /// </summary>
    [ProducesResponseType(typeof(BudgetDto), 204)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBudgetCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    ///     Обновление лимита
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBudgetCommand command)
    {
        command.CategoryId = id;
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Удаление лимита
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBudgetCommand
        {
            BudgetId = id
        };
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Получение всех лимитов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BudgetDto>), 200)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll([FromQuery] GetBudgetQuery query)
    {
        var transactions = await mediator.Send(query);
        return Ok(transactions);
    }

    /// <summary>
    ///     Получение лимита по ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DetailedBudgetDto), 200)]
    public async Task<ActionResult<DetailedCategoryDto>> GetById(Guid id)
    {
        var query = new GetBudgetByIdQuery
        {
            BudgetId = id
        };
        var transaction = await mediator.Send(query);
        return transaction == null ? NotFound() : Ok(transaction);
    }
}