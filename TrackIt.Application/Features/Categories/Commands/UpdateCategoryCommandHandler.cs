using MediatR;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Categories.Commands;

/// <summary>
///     Обработчик команды <see cref="UpdateCategoryCommand"/>
/// </summary> 
internal sealed class UpdateCategoryCommandHandler(ICategoryService service) : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    /// <inheritdoc />
    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await service.UpdateAsync(request, cancellationToken);
    }
}