using MediatR;

namespace TrackIt.Application.Features.Categories.Commands;

/// <summary>
///     Запрос на удаление категории
/// </summary>
public class DeleteCategoryCommand : IRequest<bool>
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid CategoryId { get; set; }
}