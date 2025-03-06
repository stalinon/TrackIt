using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Помощь
/// </summary>
internal sealed class HelpCommand(IEnumerable<IBotCommand> commands) : BotCommand
{
    /// <inheritdoc />
    public override string Command => "/help";
    
    /// <inheritdoc />
    public override string Description => "Показать список доступных команд.";
    
    /// <inheritdoc />
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args)
    {
        var helpText = "📜 *Список доступных команд:*\n\n";
        helpText += string.Join("\n", commands.Select(cmd => $"🔹 `{cmd.Command}` : {cmd.Description}"));
        helpText += "\n\nЕсли параметр имеет пробелы, то следует оборачивать его в кавычки `\"` во избежание неверной обработки команды.";
        await botClient.SendMessage(message.Chat.Id, EscapeTelegramMarkdownV2(helpText), ParseMode.MarkdownV2);
    }
}
