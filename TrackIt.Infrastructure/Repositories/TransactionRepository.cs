using Microsoft.EntityFrameworkCore;
using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <summary>
///     Реализация репозитория для работы с транзакциями.
/// </summary>
internal sealed class TransactionRepository : GenericRepository<TransactionEntity>, ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    /// <inheritdoc cref="TransactionRepository" />
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}