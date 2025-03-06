using System.Windows.Input;
using Telegram.Bot;
using Telegram.Bot.Types;
using TrackIt.Application.Interfaces;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Команда с авторизацией
/// </summary>
internal abstract class AuthorizedCommand(IUserContext userContext) : BotCommand
{
    /// <inheritdoc />
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args)
    {
        if (userContext.Email == null)
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Используйте /link для привязки своего аккаунта");
        }
    }
}