using TrackIt.Application.DTOs.Categories;
using TrackIt.Application.Features.Categories.Commands;
using TrackIt.Application.Features.Categories.Queries;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="ICategoryService" />
internal sealed class CategoryService(IUnitOfWork unitOfWork, IUserContext userContext) : ICategoryService
{
    /// <inheritdoc />
    public async Task<CategoryDto> CreateAsync(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        // Создание новой транзакции
        var category = new CategoryEntity
        {
            Id = Guid.NewGuid(),
            UserId = userContext.UserId,
            Name = command.Name,
            Type = command.Type
        };

        var exists = await unitOfWork.Categories.ExistsAsync(c =>
            c.UserId == category.UserId && c.Type == category.Type && c.Name == category.Name);
        if (exists)
        {
            throw new Exception($"Конфликт: категория с названием `{category.Name}:{category.Type}` уже существует");
        }

        // Добавляем в БД
        await unitOfWork.Categories.AddAsync(category);
        await unitOfWork.SaveChangesAsync();

        // Возвращаем DTO
        return new CategoryDto
        {
            Id = category.Id,
            UserId = category.UserId,
            Name = command.Name,
            Type = command.Type
        };
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var category = await unitOfWork.Categories.GetByIdAsync(command.CategoryId);
        
        if (category == null || category.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Категория не найдена или доступ запрещен.");
        }

        unitOfWork.Categories.Remove(category);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<CategoryDto> UpdateAsync(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        // Получаем транзакцию из БД
        var category = await unitOfWork.Categories.GetByIdAsync(command.CategoryId);
        if (category == null || category.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("Категория не найдена или доступ запрещен.");
        }

        // Обновляем данные
        category.Name = command.Name;
        category.Type = command.Type;
        
        var exists = await unitOfWork.Categories.ExistsAsync(c =>
            c.UserId == category.UserId && c.Type == category.Type && c.Name == category.Name);
        if (exists)
        {
            throw new Exception($"Конфликт: категория с названием `{category.Name}:{category.Type}` уже существует");
        }

        unitOfWork.Categories.Update(category);
        await unitOfWork.SaveChangesAsync();

        // Возвращаем обновленный объект
        return new CategoryDto
        {
            Id = category.Id,
            UserId = category.UserId,
            Name = category.Name,
            Type = category.Type
        };
    }

    /// <inheritdoc />
    public async Task<DetailedCategoryDto?> GetByIdAsync(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(query.CategoryId);

        if (category == null || category.UserId != userContext.UserId)
        {
            return null;
        }

        return new DetailedCategoryDto
        {
            Id = category.Id,
            UserId = category.UserId,
            Name = category.Name,
            Type = category.Type,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> ListAsync(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var transactions = await unitOfWork.Categories.GetPaginatedAsync(
            pageIndex: query.PageIndex,
            pageSize: query.Limit,
            filter: e => e.UserId == userContext.UserId,
            orderBy: e => e.UpdatedAt);

        return transactions.Select(category => new CategoryDto
        {
            Id = category.Id,
            UserId = category.UserId,
            Name = category.Name,
            Type = category.Type
        }).ToList();
    }
}