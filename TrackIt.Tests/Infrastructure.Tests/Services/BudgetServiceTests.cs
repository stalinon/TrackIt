using FluentAssertions;
using Moq;
using TrackIt.Application.Features.Budgets.Commands;
using TrackIt.Application.Features.Budgets.Queries;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Services;
using Xunit;

namespace TrackIt.Tests.Infrastructure.Tests.Services;

/// <summary>
/// Тесты для класса <see cref="BudgetService"/>.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Тестирование создания бюджета.</item>
/// <item>Тестирование удаления бюджета.</item>
/// <item>Тестирование обновления бюджета.</item>
/// <item>Тестирование получения бюджета по ID.</item>
/// <item>Тестирование получения списка бюджетов.</item>
/// <item>Тестирование проверки превышения лимита.</item>
/// </list>
/// </remarks>
public class BudgetServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<ITelegramNotificationService> _notificationServiceMock;
    private readonly BudgetService _budgetService;

    /// <inheritdoc cref="BudgetServiceTests" />
    public BudgetServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userContextMock = new Mock<IUserContext>();
        _notificationServiceMock = new Mock<ITelegramNotificationService>();

        _userContextMock.Setup(u => u.UserId).Returns(Guid.NewGuid());

        _budgetService = new BudgetService(
            _unitOfWorkMock.Object, 
            _userContextMock.Object, 
            _notificationServiceMock.Object
        );
    }

    /// <summary>
    /// Тест-кейс 1: Создание бюджета. Успешное создание.
    /// </summary>
    [Fact]
    public async Task CreateAsync_ValidCommand_ReturnsCreatedBudget()
    {
        var command = new CreateBudgetCommand { CategoryId = Guid.NewGuid(), LimitAmount = 1000 };
        var cancellationToken = CancellationToken.None;

        _unitOfWorkMock.Setup(u => u.Budgets.AddAsync(It.IsAny<BudgetEntity>()));
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

        var result = await _budgetService.CreateAsync(command, cancellationToken);

        result.Should().NotBeNull();
        result.LimitAmount.Should().Be(1000);
    }

    /// <summary>
    /// Тест-кейс 2: Удаление бюджета. Успешное удаление.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_ExistingBudget_DeletesSuccessfully()
    {
        var budgetId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var budget = new BudgetEntity { Id = budgetId, UserId = userId };

        _unitOfWorkMock.Setup(u => u.Budgets.GetByIdAsync(budgetId)).ReturnsAsync(budget);

        var result = await _budgetService.DeleteAsync(new DeleteBudgetCommand { BudgetId = budgetId }, CancellationToken.None);

        result.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.Budgets.Remove(budget), Times.Once);
    }

    /// <summary>
    /// Тест-кейс 3: Обновление бюджета. Успешное обновление.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_ExistingBudget_UpdatesSuccessfully()
    {
        var budgetId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var budget = new BudgetEntity { Id = budgetId, UserId = userId, LimitAmount = 1000 };
        var command = new UpdateBudgetCommand { BudgetId = budgetId, LimitAmount = 2000 };

        _unitOfWorkMock.Setup(u => u.Budgets.GetByIdAsync(budgetId)).ReturnsAsync(budget);

        var result = await _budgetService.UpdateAsync(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.LimitAmount.Should().Be(2000);
        _unitOfWorkMock.Verify(u => u.Budgets.Update(budget), Times.Once);
    }

    /// <summary>
    /// Тест-кейс 4: Получение бюджета по ID. Успешное получение.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_ExistingBudget_ReturnsBudget()
    {
        var budgetId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var budget = new BudgetEntity { Id = budgetId, UserId = userId, LimitAmount = 500 };

        _unitOfWorkMock.Setup(u => u.Budgets.GetByIdAsync(budgetId)).ReturnsAsync(budget);

        var result = await _budgetService.GetByIdAsync(new GetBudgetByIdQuery { BudgetId = budgetId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.LimitAmount.Should().Be(500);
    }

    /// <summary>
    /// Тест-кейс 5: Проверка превышения лимита. Отправка уведомления.
    /// </summary>
    [Fact(Skip = "Not implemented yet")]
    public async Task CheckLimitAsync_LimitExceeded_SendsNotification()
    {
        var transactionId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var transaction = new TransactionEntity { Id = transactionId, UserId = userId, Amount = 1500, Date = DateTime.UtcNow };
        var category = new CategoryEntity { Name = "Food", Budgets = new List<BudgetEntity> { new() { Id = transactionId, LimitAmount = 1000 } } };

        transaction.Category = category;
        _unitOfWorkMock.Setup(u => u.Transactions.GetByIdAsync(transactionId)).ReturnsAsync(transaction);

        await _budgetService.CheckLimitAsync(transactionId, CancellationToken.None);

        _notificationServiceMock.Verify(n => n.SendNotificationAsync(userId, It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}