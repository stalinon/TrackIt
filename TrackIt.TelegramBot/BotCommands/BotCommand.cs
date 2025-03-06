using Telegram.Bot;
using Telegram.Bot.Types;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Базовая команда 
/// </summary>
internal abstract class BotCommand : IBotCommand
{
    /// <inheritdoc />
    public abstract string Command { get; }
    
    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args);
    
    protected static string EscapeTelegramMarkdownV2(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return text
            .Replace("\\", "\\\\") 
            .Replace("_", "\\_")
            .Replace("[", "\\[")
            .Replace("]", "\\]")
            .Replace("(", "\\(")
            .Replace(")", "\\)")
            .Replace("~", "\\~")
            .Replace(">", "\\>")
            .Replace("#", "\\#")
            .Replace("+", "\\+")
            .Replace("-", "\\-")
            .Replace("=", "\\=")
            .Replace("|", "\\|")
            .Replace("{", "\\{")
            .Replace("}", "\\}")
            .Replace(".", "\\.")
            .Replace("!", "\\!");
    }

}