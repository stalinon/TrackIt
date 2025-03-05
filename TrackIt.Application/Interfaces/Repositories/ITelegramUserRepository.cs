using TrackIt.Domain.Entities;

namespace TrackIt.Application.Interfaces.Repositories;

/// <summary>
///     Репозиторий пользователя Телеграма
/// </summary>
public interface ITelegramUserRepository : IGenericRepository<TelegramUserEntity>;