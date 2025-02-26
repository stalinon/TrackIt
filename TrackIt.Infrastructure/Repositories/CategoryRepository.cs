using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="ICategoryRepository" />
internal sealed class CategoryRepository : GenericRepository<CategoryEntity>, ICategoryRepository
{
    /// <inheritdoc cref="CategoryRepository" />
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}