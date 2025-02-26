using TrackIt.Domain.Entities;

namespace TrackIt.Application.Interfaces.Repositories;

/// <summary>
///     Интерфейс репозитория для работы с транзакциями.
/// </summary>
public interface ITransactionRepository : IGenericRepository<TransactionEntity>;