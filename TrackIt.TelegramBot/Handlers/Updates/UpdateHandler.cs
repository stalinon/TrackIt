using Telegram.Bot;
using Telegram.Bot.Types;

namespace TrackIt.TelegramBot.Handlers.Updates;

/// <summary>
///     Обработчик обновлений от телеграмма
/// </summary>
public abstract class UpdateHandler : Handler
{
    /// <summary>
    ///     Обработать
    /// </summary>
    protected virtual Task Handle(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (Next is UpdateHandler nextUpdate)
        {
            return nextUpdate.Handle(botClient, update, cancellationToken);
        }
        
        return Task.CompletedTask;
    }
    
    public static implicit operator Func<ITelegramBotClient, Update, CancellationToken, Task>(UpdateHandler handler)
    {
        return handler.Handle;
    }
}