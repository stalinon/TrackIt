using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Domain.Common;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Persistence;
using TrackIt.Infrastructure.Repositories;
using Xunit;

namespace TrackIt.Tests.Infrastructure.Tests.Repositories;

/// <summary>
/// Тесты для класса <see cref="GenericRepository{T}"/>.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Тестирование получения сущности по Id.</item>
/// <item>Тестирование получения всех сущностей.</item>
/// <item>Тестирование поиска по условию.</item>
/// <item>Тестирование проверки существования.</item>
/// <item>Тестирование подсчета записей.</item>
/// <item>Тестирование пагинации.</item>
/// <item>Тестирование добавления сущности.</item>
/// <item>Тестирование обновления сущности.</item>
/// <item>Тестирование удаления сущности.</item>
/// </list>
/// </remarks>
public class GenericRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly ICategoryRepository _repository;
    private readonly List<CategoryEntity> _testData;

    /// <inheritdoc cref="GenericRepositoryTests" />
    public GenericRepositoryTests()
    {
        _testData =
        [
            new() { Id = Guid.NewGuid(), Name = "Entity1" },
            new() { Id = Guid.NewGuid(), Name = "Entity2" }
        ];

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb") // Используем InMemory БД для тестов
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new CategoryRepository(_context);
    }

    /// <summary>
    /// Тест-кейс 1: Получение сущности по Id. Существующая сущность.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_ExistingEntity_ReturnsEntity()
    {
        var entity = _testData.First();
        _context.Add(entity);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(entity.Id);

        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
    }

    /// <summary>
    /// Тест-кейс 2: Получение сущности по Id. Несуществующая сущность.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_NonExistingEntity_ReturnsNull()
    {
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    /// <summary>
    /// Тест-кейс 3: Получение всех сущностей.
    /// </summary>
    [Fact(Skip = "Not implemented yet")]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        _context.AddRange(_testData);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();

        Assert.Equal(_testData.Count, result.Count());
    }

    /// <summary>
    /// Тест-кейс 4: Проверка существования сущности. Существующая запись.
    /// </summary>
    [Fact]
    public async Task ExistsAsync_ExistingEntity_ReturnsTrue()
    {
        _context.AddRange(_testData);

        var result = await _repository.ExistsAsync(e => e.Name == "Entity1");

        Assert.True(result);
    }

    /// <summary>
    /// Тест-кейс 5: Добавление сущности.
    /// </summary>
    [Fact]
    public async Task AddAsync_AddsEntityToDbSet()
    {
        var entity = new CategoryEntity { Id = Guid.NewGuid(), Name = "NewEntity" };

        await _repository.AddAsync(entity);
        await _context.SaveChangesAsync();

        Assert.True(_context.Set<CategoryEntity>().Any(e => e.Id == entity.Id));
    }

    /// <summary>
    /// Тест-кейс 6: Удаление сущности.
    /// </summary>
    [Fact]
    public async Task Remove_RemovesEntityFromDbSet()
    {
        _context.AddRange(_testData);
        await _context.SaveChangesAsync();
        
        var entity = _testData.First();

        _repository.Remove(entity);
        await _context.SaveChangesAsync();

        Assert.False(_context.Set<CategoryEntity>().Any(e => e.Id == entity.Id));
    }
    
    /// <summary>
    /// Тест-кейс 7: Подсчет записей. Без фильтра.
    /// </summary>
    [Fact]
    public async Task CountAsync_WithoutFilter_ReturnsTotalCount()
    {
        _context.AddRange(_testData);
        await _context.SaveChangesAsync();

        var result = await _repository.CountAsync();

        Assert.Equal(_testData.Count, result);
    }

    /// <summary>
    /// Тест-кейс 8: Обновление сущности.
    /// </summary>
    [Fact]
    public async Task Update_UpdatesEntityInDbSet()
    {
        _context.AddRange(_testData);
        await _context.SaveChangesAsync();
        
        var entity = _testData.First();
        entity.Name = "UpdatedEntity";

        _repository.Update(entity);
        await _context.SaveChangesAsync();

        Assert.Equal(_context.Find<CategoryEntity>(entity.Id)!.Name, entity.Name);
    }

    /// <summary>
    /// Тест-кейс 9: Пагинация. Возвращает ожидаемое количество записей.
    /// </summary>
    [Fact]
    public async Task GetPaginatedAsync_ValidPage_ReturnsCorrectData()
    {
        _context.AddRange(_testData);
        await _context.SaveChangesAsync();

        const int pageIndex = 1;
        const int pageSize = 1;
        var result = await _repository.GetPaginatedAsync(pageIndex, pageSize);

        Assert.Single(result);
    }

}
