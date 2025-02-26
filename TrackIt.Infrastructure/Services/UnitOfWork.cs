using TrackIt.Application.Interfaces;
using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Infrastructure.Persistence;
using TrackIt.Infrastructure.Repositories;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IUnitOfWork" />
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    /// <inheritdoc />
    public IUserRepository Users { get; }
    
    /// <inheritdoc />
    public ITransactionRepository Transactions { get; }
    
    /// <inheritdoc />
    public ICategoryRepository Categories { get; }

    /// <inheritdoc cref="UnitOfWork" />
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Transactions = new TransactionRepository(_context);
        Users = new UserRepository(_context);
        Categories = new CategoryRepository(_context);
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    
    /// <inheritdoc />
    public void Dispose() => _context.Dispose();
}