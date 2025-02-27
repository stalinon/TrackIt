namespace TrackIt.Application.Interfaces;

/// <summary>
///     Сервис уведомлений через Telegram
/// </summary>
public interface ITelegramNotificationService
{
    /// <summary>
    ///     Отправить уведомление пользователю о предстоящем платеже
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="message">Сообщение</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task SendNotificationAsync(Guid userId, string message, CancellationToken cancellationToken);
}