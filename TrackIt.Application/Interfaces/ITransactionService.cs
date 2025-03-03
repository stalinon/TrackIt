using TrackIt.Application.DTOs;
using TrackIt.Application.DTOs.Transactions;
using TrackIt.Application.Features.Transactions.Commands;
using TrackIt.Application.Features.Transactions.Queries;

namespace TrackIt.Application.Interfaces;

/// <summary>
///     Сервис транзакций
/// </summary>
public interface ITransactionService
{
    /// <summary>
    ///     Создать транзакцию
    /// </summary>
    Task<TransactionDto> CreateAsync(CreateTransactionCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить транзакцию
    /// </summary>
    Task<bool> DeleteAsync(DeleteTransactionCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить транзакцию
    /// </summary>
    Task<TransactionDto> UpdateAsync(UpdateTransactionCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить транзакцию
    /// </summary>
    Task<DetailedTransactionDto?> GetByIdAsync(GetTransactionByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список транзакций
    /// </summary>
    Task<PagedList<TransactionDto>>
        ListAsync(GetTransactionsQuery query, CancellationToken cancellationToken);
}