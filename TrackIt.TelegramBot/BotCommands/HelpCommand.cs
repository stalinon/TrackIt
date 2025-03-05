using Telegram.Bot;
using Telegram.Bot.Types;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Помощь
/// </summary>
internal sealed class HelpCommand(IEnumerable<IBotCommand> commands) : IBotCommand
{
    /// <inheritdoc />
    public string Command => "/help";
    
    /// <inheritdoc />
    public string Description => "Показать список доступных команд.";
    
    /// <inheritdoc />
    public async Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args)
    {
        var helpText = "📜 *Список доступных команд:*\n\n";
        helpText += string.Join("\n", commands.Select(cmd => $"🔹 `{cmd.Command}` - {cmd.Description}"));
        await botClient.SendMessage(message.Chat.Id, helpText, Telegram.Bot.Types.Enums.ParseMode.Markdown);
    }
}
