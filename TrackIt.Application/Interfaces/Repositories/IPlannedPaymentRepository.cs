using TrackIt.Domain.Entities;

namespace TrackIt.Application.Interfaces.Repositories;

/// <summary>
///     Интерфейс репозитория для работы с платежами.
/// </summary>
public interface IPlannedPaymentRepository : IGenericRepository<PlannedPaymentEntity>;