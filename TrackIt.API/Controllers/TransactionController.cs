using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Features.Transactions.Commands;
using TrackIt.Application.Features.Transactions.Queries;
using TrackIt.Application.Interfaces;

namespace TrackIt.API.Controllers;

/// <summary>
///     Контроллер транзакций
/// </summary>
[Route("api/transactions")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _context;

    /// <inheritdoc cref="TransactionController" />
    public TransactionController(IMediator mediator, IUserContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    /// <summary>
    ///     Создание транзакции
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
    {
        command.UserId = _context.UserId;
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    ///     Обновление транзакции
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionCommand command)
    {
        command.TransactionId = id;
        command.UserId = _context.UserId;
        await _mediator.Send(command);
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
            TransactionId = id,
            UserId = _context.UserId
        };
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Получение всех транзакций
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll(int page = 0, int limit = 25)
    {
        var query = new GetTransactionsQuery
        {
            UserId = _context.UserId,
            PageIndex = page,
            Limit = limit
        };
        var transactions = await _mediator.Send(query);
        return Ok(transactions);
    }

    /// <summary>
    ///     Получение транзакции по ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDto>> GetById(Guid id)
    {
        var query = new GetTransactionByIdQuery
        {
            TransactionId = id,
            UserId = _context.UserId
        };
        var transaction = await _mediator.Send(query);
        return transaction == null ? NotFound() : Ok(transaction);
    }
}