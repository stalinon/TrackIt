using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using TrackIt.Application.Features.PlannedPayments.Commands;
using TrackIt.Application.Features.PlannedPayments.Queries;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Services;
using Xunit;

namespace TrackIt.Tests.Infrastructure.Tests.Services;

/// <summary>
/// Тесты для класса <see cref="PlannedPaymentService"/>.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Тестирование создания запланированного платежа.</item>
/// <item>Тестирование удаления запланированного платежа.</item>
/// <item>Тестирование обновления запланированного платежа.</item>
/// <item>Тестирование получения запланированного платежа по ID.</item>
/// <item>Тестирование получения списка запланированных платежей.</item>
/// </list>
/// </remarks>
public class PlannedPaymentServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly PlannedPaymentService _plannedPaymentService;

    /// <inheritdoc cref="PlannedPaymentServiceTests" />
    public PlannedPaymentServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userContextMock = new Mock<IUserContext>();
        Mock<ITelegramNotificationService> notificationServiceMock = new();

        _userContextMock.Setup(u => u.UserId).Returns(Guid.NewGuid());

        _plannedPaymentService = new PlannedPaymentService(
            _unitOfWorkMock.Object, 
            _userContextMock.Object, 
            notificationServiceMock.Object
        );
    }

    /// <summary>
    /// Тест-кейс 1: Создание платежа. Успешное создание.
    /// </summary>
    [Fact(Skip = "Not implemented yet")]
    public async Task CreateAsync_ValidCommand_ReturnsCreatedPayment()
    {
        var command = new CreatePlannedPaymentCommand { Amount = 1000, CategoryId = Guid.NewGuid(), Description = "Test Payment", DueDate = DateTime.Now };
        var cancellationToken = CancellationToken.None;

        _unitOfWorkMock.Setup(u => u.PlannedPayments.AddAsync(It.IsAny<PlannedPaymentEntity>()));
        _unitOfWorkMock.Setup(u => u.PlannedPayments.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(new PlannedPaymentEntity { Amount = command.Amount, CategoryId = command.CategoryId, DueDate = command.DueDate })!);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

        var result = await _plannedPaymentService.CreateAsync(command, cancellationToken);
        
        result.Should().NotBeNull();
        result.Amount.Should().Be(1000);
    }

    /// <summary>
    /// Тест-кейс 2: Удаление платежа. Успешное удаление.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_ExistingPayment_DeletesSuccessfully()
    {
        var paymentId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var payment = new PlannedPaymentEntity { Id = paymentId, UserId = userId };

        _unitOfWorkMock.Setup(u => u.PlannedPayments.GetByIdAsync(paymentId)).ReturnsAsync(payment);

        var result = await _plannedPaymentService.DeleteAsync(new DeletePlannedPaymentCommand { PlannedPaymentId = paymentId }, CancellationToken.None);

        result.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.PlannedPayments.Remove(payment), Times.Once);
    }

    /// <summary>
    /// Тест-кейс 3: Обновление платежа. Успешное обновление.
    /// </summary>
    [Fact(Skip = "Not implemented yet")]s
    public async Task UpdateAsync_ExistingPayment_UpdatesSuccessfully()
    {
        var paymentId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var payment = new PlannedPaymentEntity { Id = paymentId, UserId = userId, Amount = 1000 };
        var command = new UpdatePlannedPaymentCommand { PlannedPaymentId = paymentId, Amount = 2000 };

        _unitOfWorkMock.Setup(u => u.PlannedPayments.GetByIdAsync(paymentId)).ReturnsAsync(payment);

        var result = await _plannedPaymentService.UpdateAsync(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Amount.Should().Be(2000);
        _unitOfWorkMock.Verify(u => u.PlannedPayments.Update(payment), Times.Once);
    }

    /// <summary>
    /// Тест-кейс 4: Получение платежа по ID. Успешное получение.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_ExistingPayment_ReturnsPayment()
    {
        var paymentId = Guid.NewGuid();
        var userId = _userContextMock.Object.UserId;
        var payment = new PlannedPaymentEntity { Id = paymentId, UserId = userId, Amount = 500 };

        _unitOfWorkMock.Setup(u => u.PlannedPayments.GetByIdAsync(paymentId)).ReturnsAsync(payment);

        var result = await _plannedPaymentService.GetByIdAsync(new GetPlannedPaymentByIdQuery { PlannedPaymentId = paymentId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Amount.Should().Be(500);
    }

    /// <summary>
    /// Тест-кейс 5: Получение списка платежей. Возвращает ожидаемый список.
    /// </summary>
    [Fact]
    public async Task ListAsync_ReturnsPaymentList()
    {
        var userId = _userContextMock.Object.UserId;
        var payments = new List<PlannedPaymentEntity>
        {
            new() { Id = Guid.NewGuid(), UserId = userId, Amount = 1000 },
            new() { Id = Guid.NewGuid(), UserId = userId, Amount = 500 }
        };

        _unitOfWorkMock.Setup(u => u.PlannedPayments.GetPaginatedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<PlannedPaymentEntity, bool>>>(), It.IsAny<Expression<Func<PlannedPaymentEntity, object>>>(), It.IsAny<bool>()))
            .ReturnsAsync(payments);

        var result = await _plannedPaymentService.ListAsync(new GetPlannedPaymentQuery { PageIndex = 1, Limit = 10 }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
    }
}