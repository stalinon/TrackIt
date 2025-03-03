using MediatR;
using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Categories.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetCategoriesQuery" />
/// </summary>
internal sealed class GetCategoriesQueryHandlerHandler(ICategoryService service) : IRequestHandler<GetCategoriesQuery, PagedList<CategoryDto>>
{
    /// <inheritdoc />
    public async Task<PagedList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}