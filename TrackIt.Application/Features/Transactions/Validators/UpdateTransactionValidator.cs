using FluentValidation;
using TrackIt.Application.Features.Transactions.Commands;

namespace TrackIt.Application.Features.Transactions.Validators;

/// <summary>
///     Валидатор команды <see cref="UpdateTransactionCommand"/>.
/// </summary>
public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionCommand>
{
    /// <summary>
    ///     Инициализирует новый экземпляр <see cref="UpdateTransactionValidator"/>.
    /// </summary>
    public UpdateTransactionValidator()
    {
        RuleFor(t => t.TransactionId)
            .NotEmpty().WithMessage("Идентификатор транзакции обязателен.");

        RuleFor(t => t.Amount)
            .GreaterThan(0).WithMessage("Сумма транзакции должна быть больше 0.");
    }
}