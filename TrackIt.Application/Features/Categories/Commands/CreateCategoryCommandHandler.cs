using MediatR;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Categories.Commands;

/// <summary>
///     Обработчик команды <see cref="CreateCategoryCommand" />
/// </summary>
internal sealed class CreateCategoryCommandHandler(ICategoryService service) : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    /// <inheritdoc />
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await service.CreateAsync(request, cancellationToken);
    }
}