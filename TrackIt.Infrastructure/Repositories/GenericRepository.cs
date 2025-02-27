using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Domain.Common;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="IGenericRepository{T}" />
internal class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
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
    public virtual IQueryable<T> GetQuery() => DbSet;

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(Guid id) => await GetQuery().FirstOrDefaultAsync(e => e.Id == id);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetAllAsync() => await GetQuery().ToListAsync();

    /// <inheritdoc />
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await GetQuery().Where(predicate).ToListAsync();

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) => await GetQuery().AnyAsync(predicate);

    /// <inheritdoc />
    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null) => predicate == null ? await GetQuery().CountAsync() : await GetQuery().CountAsync(predicate);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetPaginatedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false)
    {
        var query = GetQuery();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy == null)
        {
            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        if (orderByDescending)
        {
            query = query.OrderByDescending(orderBy);
        }
        else
        {
            query = query.OrderBy(orderBy);
        }

        return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <inheritdoc />
    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
    
    /// <inheritdoc />
    public void Update(T entity) => DbSet.Update(entity);
    
    /// <inheritdoc />
    public void Remove(T entity) => DbSet.Remove(entity);
}