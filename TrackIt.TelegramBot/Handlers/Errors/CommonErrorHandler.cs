using Microsoft.Extensions.Logging;
using Telegram.Bot;
using TrackIt.TelegramBot.Services;

namespace TrackIt.TelegramBot.Handlers.Errors;

/// <summary>
///     Общий обработчик ошибок
/// </summary>
internal sealed class CommonErrorHandler(ILogger<TelegramBotAdapter> logger) : ErrorHandler
{
    /// <inheritdoc cref="CommonErrorHandler" />
    protected override Task Handle(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is Exception innerException)
        {
            logger.LogError(exception, $"Возникла ошибка в боте: {innerException.Message}");
        }
        
        return base.Handle(botClient, exception, cancellationToken);
    }
}