using FluentValidation;
using TrackIt.Application.Features.Transactions.Commands;

namespace TrackIt.Application.Features.Transactions.Validators;

/// <summary>
///     Валидатор команды <see cref="CreateTransactionCommand"/>.
/// </summary>
public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    /// <summary>
    ///     Инициализирует новый экземпляр <see cref="CreateTransactionValidator"/>.
    /// </summary>
    public CreateTransactionValidator()
    {
        RuleFor(t => t.Amount)
            .GreaterThan(0).WithMessage("Сумма транзакции должна быть больше 0.");

        RuleFor(t => t.CategoryId)
            .NotEmpty().WithMessage("Категория обязательна.");
    }
}