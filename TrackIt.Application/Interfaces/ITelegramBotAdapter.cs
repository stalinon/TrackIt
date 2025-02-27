namespace TrackIt.Application.Interfaces;

/// <summary>
///     Адаптер для работы с Tg
/// </summary>
public interface ITelegramBotAdapter
{
    /// <summary>
    ///     Отправить сообщение
    /// </summary>
    Task SendMessageAsync(long chatId, string message);
}