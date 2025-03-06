using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using TrackIt.Application.Features.Categories.Commands;
using TrackIt.Application.Features.Categories.Queries;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;
using TrackIt.Domain.Enums;
using TrackIt.Infrastructure.Services;
using Xunit;

namespace TrackIt.Tests.Infrastructure.Tests.Services;

/// <summary>
/// Тесты для класса <see cref="CategoryService"/>.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Тестирование создания категории.</item>
/// <item>Тестирование удаления категории.</item>
/// <item>Тестирование обновления категории.</item>
/// <item>Тестирование получения категории по ID.</item>
/// <item>Тестирование получения списка категорий.</item>
/// </list>
/// </remarks>
public class CategoryServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly CategoryService _categoryService;

    /// <inheritdoc cref="CategoryServiceTests" />
    public CategoryServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userContextMock = new Mock<IUserContext>();

        _userContextMock.Setup(u => u.UserId).Returns(Guid.NewGuid());

        _categoryService = new CategoryService(
            _unitOfWorkMock.Object, 
            _userContextMock.Object
        );
    }

    /// <summary>
    /// Тест-кейс 1: Создание категории. Успешное создание.
    /// </summary>
    [Fact]
    public async Task CreateAsync_ValidCommand_ReturnsCreatedCategory()
    {
        var command = new CreateCategoryCommand { Name = "Food", Type = CategoryType.EXPENSE };
        var cancellationToken = CancellationToken.None;

        _unitOfWorkMock.Setup(u => u.Categories.ExistsAsync(It.IsAny<Expression<Func<CategoryEntity, bool>>>())).ReturnsAsync(false);
        _unitOfWorkMock.Setup(u => u.Categories.AddAsync(It.IsAny<CategoryEntity>()));
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

        var result = await _categoryService.CreateAsync(command, cancellationToken);

        result.Should().NotBeNull();
        result.Name.Should().Be("Food");
        result.Type.Should().Be(CategoryType.EXPENSE);
    }

    /// <summary>
    /// Тест-кейс 2: Удаление категории. Успешное удаление.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_ExistingCategory_DeletesSuccessfully()
    {
        var categoryId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var category = new CategoryEntity { Id = categoryId, UserId = userId };

        _unitOfWorkMock.Setup(u => u.Categories.GetByIdAsync(categoryId)).ReturnsAsync(category);

        var result = await _categoryService.DeleteAsync(new DeleteCategoryCommand { CategoryId = categoryId }, CancellationToken.None);

        result.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.Categories.Remove(category), Times.Once);
    }

    /// <summary>
    /// Тест-кейс 3: Обновление категории. Успешное обновление.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_ExistingCategory_UpdatesSuccessfully()
    {
        var categoryId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var category = new CategoryEntity { Id = categoryId, UserId = userId, Name = "OldName", Type = CategoryType.EXPENSE };
        var command = new UpdateCategoryCommand { CategoryId = categoryId, Name = "NewName", Type = CategoryType.INCOME };

        _unitOfWorkMock.Setup(u => u.Categories.GetByIdAsync(categoryId)).ReturnsAsync(category);
        _unitOfWorkMock.Setup(u => u.Categories.ExistsAsync(It.IsAny<Expression<Func<CategoryEntity, bool>>>())).ReturnsAsync(false);

        var result = await _categoryService.UpdateAsync(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be("NewName");
        result.Type.Should().Be(CategoryType.INCOME);
        _unitOfWorkMock.Verify(u => u.Categories.Update(category), Times.Once);
    }

    /// <summary>
    /// Тест-кейс 4: Получение категории по ID. Успешное получение.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_ExistingCategory_ReturnsCategory()
    {
        var categoryId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var category = new CategoryEntity { Id = categoryId, UserId = userId, Name = "Transport", Type = CategoryType.EXPENSE };

        _unitOfWorkMock.Setup(u => u.Categories.GetByIdAsync(categoryId)).ReturnsAsync(category);

        var result = await _categoryService.GetByIdAsync(new GetCategoryByIdQuery { CategoryId = categoryId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be("Transport");
    }

    /// <summary>
    /// Тест-кейс 5: Получение списка категорий. Возвращает ожидаемый список.
    /// </summary>
    [Fact]
    public async Task ListAsync_ReturnsCategoryList()
    {
        var userId = _userContextMock.Object.UserId;
        var categories = new List<CategoryEntity>
        {
            new() { Id = Guid.NewGuid(), UserId = userId, Name = "Groceries", Type = CategoryType.EXPENSE },
            new() { Id = Guid.NewGuid(), UserId = userId, Name = "Salary", Type = CategoryType.INCOME }
        };

        _unitOfWorkMock.Setup(u => u.Categories.GetPaginatedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<CategoryEntity, bool>>>(), It.IsAny<Expression<Func<CategoryEntity, object>>>(), It.IsAny<bool>()))
            .ReturnsAsync(categories);
        _unitOfWorkMock.Setup(u => u.Categories.CountAsync(It.IsAny<Expression<Func<CategoryEntity, bool>>>())).ReturnsAsync(categories.Count);

        var result = await _categoryService.ListAsync(new GetCategoriesQuery { PageIndex = 1, Limit = 10 }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
    }
}