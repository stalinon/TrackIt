using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrackIt.Domain.Interfaces.Repositories;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="IGenericRepository{T}" />
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;

    /// <inheritdoc cref="GenericRepository{T}" />
    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(Guid id) => await DbSet.FindAsync(id);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetAllAsync() => await DbSet.ToListAsync();

    /// <inheritdoc />
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await DbSet.Where(predicate).ToListAsync();

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) => await DbSet.AnyAsync(predicate);

    /// <inheritdoc />
    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null) => predicate == null ? await DbSet.CountAsync() : await DbSet.CountAsync(predicate);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetPaginatedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false)
    {
        IQueryable<T> query = DbSet;

        if (filter != null) query = query.Where(filter);
        if (orderBy != null) query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

        return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <inheritdoc />
    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
    
    /// <inheritdoc />
    public void Update(T entity) => DbSet.Update(entity);
    
    /// <inheritdoc />
    public void Remove(T entity) => DbSet.Remove(entity);
}