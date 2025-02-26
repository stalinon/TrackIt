using MediatR;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Categories.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetCategoriesQuery" />
/// </summary>
internal sealed class GetCategoriesQueryHandlerHandler(ICategoryService service) : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}