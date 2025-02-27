using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="IBudgetRepository" />
internal sealed class BudgetRepository : GenericRepository<BudgetEntity>, IBudgetRepository
{
    /// <inheritdoc cref="BudgetRepository" />
    public BudgetRepository(ApplicationDbContext context) : base(context)
    {
    }
}