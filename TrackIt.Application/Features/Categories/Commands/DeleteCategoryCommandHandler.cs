using MediatR;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Categories.Commands;

/// <summary>
///     Обработчик команды <see cref="DeleteCategoryCommand"/>
/// </summary>
internal sealed class DeleteCategoryCommandHandler(ICategoryService service) : IRequestHandler<DeleteCategoryCommand, bool>
{
    /// <inheritdoc />
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        return await service.DeleteAsync(request, cancellationToken);
    }
}