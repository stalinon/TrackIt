using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Features.Transactions.Commands;
using TrackIt.Application.Features.Transactions.Queries;

namespace TrackIt.API.Controllers;

/// <summary>
///     Контроллер транзакций
/// </summary>
[Route("api/transactions")]
[ApiController]
public class TransactionController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Создание транзакции
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TransactionDto), 204)]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    ///     Обновление транзакции
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionCommand command)
    {
        command.TransactionId = id;
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Удаление транзакции
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTransactionCommand
        {
            TransactionId = id
        };
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Получение всех транзакций
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransactionDto>), 200)]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll([FromQuery] GetTransactionsQuery query)
    {
        var transactions = await mediator.Send(query);
        return Ok(transactions);
    }

    /// <summary>
    ///     Получение транзакции по ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DetailedTransactionDto), 200)]
    public async Task<ActionResult<DetailedTransactionDto>> GetById(Guid id)
    {
        var query = new GetTransactionByIdQuery
        {
            TransactionId = id
        };
        var transaction = await mediator.Send(query);
        return transaction == null ? NotFound() : Ok(transaction);
    }
}