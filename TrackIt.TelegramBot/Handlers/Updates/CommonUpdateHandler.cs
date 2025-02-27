using Telegram.Bot;
using Telegram.Bot.Types;
using TrackIt.Application.Interfaces;

namespace TrackIt.TelegramBot.Handlers.Updates;

/// <summary>
///     Обработчик неизвестных комманд
/// </summary>
public class CommonUpdateHandler(ITelegramBotAdapter adapter) : UpdateHandler
{
    /// <inheritdoc cref="CommonUpdateHandler" />
    protected override Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message != null)
        {
            adapter.SendMessageAsync(update.Message.Chat.Id, $"Неизвестная команда: {update.Message.Text}");
        }
        
        return base.Handle(botClient, update, cancellationToken);
    }
}