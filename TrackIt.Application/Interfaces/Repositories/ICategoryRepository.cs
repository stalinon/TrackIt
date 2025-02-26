using TrackIt.Domain.Entities;

namespace TrackIt.Application.Interfaces.Repositories;

/// <summary>
///     Репозиторий категорий
/// </summary>
public interface ICategoryRepository : IGenericRepository<CategoryEntity>;