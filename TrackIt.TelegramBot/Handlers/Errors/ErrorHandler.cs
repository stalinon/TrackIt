using Telegram.Bot;

namespace TrackIt.TelegramBot.Handlers.Errors;

/// <summary>
///     Обработчик исключений от телеграмма
/// </summary>
internal abstract class ErrorHandler : Handler
{
    /// <summary>
    ///     Обработать
    /// </summary>
    protected virtual Task Handle(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        if (Next is ErrorHandler nextUpdate)
        {
            return nextUpdate.Handle(botClient, exception, cancellationToken);
        }
        
        return Task.CompletedTask;
    }
    
    public static implicit operator Func<ITelegramBotClient, Exception, CancellationToken, Task>(ErrorHandler handler)
    {
        return handler.Handle;
    }
}