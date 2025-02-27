using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="IPlannedPaymentRepository" />
internal sealed class PlannedPaymentRepository : GenericRepository<PlannedPaymentEntity>, IPlannedPaymentRepository
{
    /// <inheritdoc cref="PlannedPaymentRepository" />
    public PlannedPaymentRepository(ApplicationDbContext context) : base(context)
    {
    }
}