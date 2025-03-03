using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Features.Categories.Commands;
using TrackIt.Application.Features.Categories.Queries;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Сервис категорий
/// </summary>
public interface ICategoryService
{
    /// <summary>
    ///     Создать категорию
    /// </summary>
    Task<CategoryDto> CreateAsync(CreateCategoryCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить категорию
    /// </summary>
    Task<bool> DeleteAsync(DeleteCategoryCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить категорию
    /// </summary>
    Task<CategoryDto> UpdateAsync(UpdateCategoryCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить категорию
    /// </summary>
    Task<DetailedCategoryDto?> GetByIdAsync(GetCategoryByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список категорий
    /// </summary>
    Task<PagedList<CategoryDto>> ListAsync(GetCategoriesQuery query, CancellationToken cancellationToken);
}