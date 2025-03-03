using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Features.Categories.Commands;
using TrackIt.Application.Features.Categories.Queries;

namespace TrackIt.API.Controllers;

/// <summary>
///     Контроллер категорий
/// </summary>
[Route("api/categories")]
[ApiController]
[Authorize]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Создание категории
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), 204)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    ///     Обновление категории
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand command)
    {
        command.CategoryId = id;
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Удаление категории
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCategoryCommand()
        {
            CategoryId = id
        };
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Получение всех категорий
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<CategoryDto>), 200)]
    public async Task<ActionResult<PagedList<CategoryDto>>> GetAll([FromQuery] GetCategoriesQuery query)
    {
        var transactions = await mediator.Send(query);
        return Ok(transactions);
    }

    /// <summary>
    ///     Получение категории по ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DetailedCategoryDto), 200)]
    public async Task<ActionResult<DetailedCategoryDto>> GetById(Guid id)
    {
        var query = new GetCategoryByIdQuery()
        {
            CategoryId = id
        };
        var transaction = await mediator.Send(query);
        return transaction == null ? NotFound() : Ok(transaction);
    }
}