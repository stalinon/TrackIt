using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Features.Categories.Commands;
using TrackIt.Application.Features.Categories.Queries;
using TrackIt.Application.Features.PlannedPayments.Commands;
using TrackIt.Application.Features.PlannedPayments.Queries;

namespace TrackIt.API.Controllers;

/// <summary>
///     Контроллер платежей
/// </summary>
[Route("api/payments")]
[ApiController]
public class PlannedPaymentController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Создание платежа
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlannedPaymentCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    ///     Обновление платежа
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlannedPaymentCommand command)
    {
        command.CategoryId = id;
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Удаление платежа
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeletePlannedPaymentCommand
        {
            PlannedPaymentId = id
        };
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Получение всех платежей
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll([FromQuery] GetPlannedPaymentQuery query)
    {
        var transactions = await mediator.Send(query);
        return Ok(transactions);
    }

    /// <summary>
    ///     Получение платежа по ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedCategoryDto>> GetById(Guid id)
    {
        var query = new GetPlannedPaymentByIdQuery
        {
            PlannedPaymentId = id
        };
        var transaction = await mediator.Send(query);
        return transaction == null ? NotFound() : Ok(transaction);
    }
}