using TrackIt.Domain.Entities;

namespace TrackIt.Application.Interfaces.Repositories;

/// <summary>
///     Сервис лимитов бюджета
/// </summary>
public interface IBudgetRepository : IGenericRepository<BudgetEntity>;