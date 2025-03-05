using Microsoft.EntityFrameworkCore.Storage;
using TrackIt.Application.Interfaces;
using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Infrastructure.Persistence;
using TrackIt.Infrastructure.Repositories;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IUnitOfWork" />
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _currentTransaction;
    
    /// <inheritdoc />
    public IUserRepository Users { get; }
    
    /// <inheritdoc />
    public ITransactionRepository Transactions { get; }
    
    /// <inheritdoc />
    public ICategoryRepository Categories { get; }
    
    /// <inheritdoc />
    public IPlannedPaymentRepository PlannedPayments { get; }
    
    /// <inheritdoc />
    public IBudgetRepository Budgets { get; }
    
    /// <inheritdoc />
    public ITelegramUserRepository TelegramUsers { get; }

    /// <inheritdoc cref="UnitOfWork" />
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Transactions = new TransactionRepository(_context);
        Users = new UserRepository(_context);
        Categories = new CategoryRepository(_context);
        PlannedPayments = new PlannedPaymentRepository(_context);
        Budgets = new BudgetRepository(_context);
        TelegramUsers = new TelegramUserRepository(_context);
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync(); 
    
    /// <inheritdoc />
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("Транзакция уже начата");
        }

        _currentTransaction = await _context.Database.BeginTransactionAsync();
        return _currentTransaction;
    }

    /// <inheritdoc />
    public async Task CommitTransactionAsync()
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("Нет активной транзакции");
        }

        try
        {
            await _context.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    /// <inheritdoc />
    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("Нет активной транзакции");
        }

        try
        {
            await _currentTransaction.RollbackAsync();
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _context.Dispose();
    }
}