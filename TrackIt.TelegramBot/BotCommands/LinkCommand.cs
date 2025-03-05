using Telegram.Bot;
using Telegram.Bot.Types;
using TrackIt.Application.Interfaces;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Команда для привязки аккаунта
/// </summary>
internal sealed class LinkCommand(IUserLinkService linkService, IUserContext userContext) : IBotCommand
{
    /// <inheritdoc />
    public string Command => "/link";
    
    /// <inheritdoc />
    public string Description => "Генерирует код для привязки Telegram к аккаунту.";

    /// <inheritdoc />
    public async Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args)
    {
        if (userContext.Email != null)
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Аккаунт уже привязан");
        }
        
        var code = linkService.GenerateLinkCode(message.From!.Id);
        await botClient.SendMessage(message.Chat.Id, 
            $"🔗 Ваш код привязки: `{code}`\n" +
            "Введите этот код на сайте.", 
            Telegram.Bot.Types.Enums.ParseMode.Markdown);
    }
}