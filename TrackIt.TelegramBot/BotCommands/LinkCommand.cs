using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TrackIt.Application.Interfaces;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Команда для привязки аккаунта
/// </summary>
internal sealed class LinkCommand(IUserLinkService linkService, IUserContext userContext) : BotCommand
{
    /// <inheritdoc />
    public override string Command => "/link";
    
    /// <inheritdoc />
    public override string Description => "Генерирует код для привязки Telegram к аккаунту.";

    /// <inheritdoc />
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args)
    {
        if (userContext.Email != null)
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Аккаунт уже привязан");
            return;
        }
        
        var code = linkService.GenerateLinkCode(message.From!.Id);
        var text = $"🔗 Ваш код привязки: `{code}`\n" +
                   "Введите этот код на сайте.";
        await botClient.SendMessage(message.Chat.Id, 
            EscapeTelegramMarkdownV2(text), 
            ParseMode.MarkdownV2);
    }
}