using MediatR;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Categories.Queries;

/// <summary>
///     Обработчик запроса <see cref="GetCategoryByIdQuery" />
/// </summary>
internal sealed class GetCategoryByIdQueryHandler(ICategoryService service) : IRequestHandler<GetCategoryByIdQuery, DetailedCategoryDto?>
{
    /// <inheritdoc />
    public async Task<DetailedCategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetByIdAsync(request, cancellationToken);
    }
}