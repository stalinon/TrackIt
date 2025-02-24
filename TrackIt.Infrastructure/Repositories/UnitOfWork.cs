using TrackIt.Application.Interfaces;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="UnitOfWork" />
internal class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    /// <inheritdoc />
    public IRepository<T> Repository<T>() where T : class
    {
        return new Repository<T>(context);
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        context.Dispose();
    }
}