using Telegram.Bot;
using Telegram.Bot.Types;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Команда для взаимодействия с ботом
/// </summary>
public interface IBotCommand
{
    /// <summary>
    ///     Триггер
    /// </summary>
    string Command { get; }
    
    /// <summary>
    ///     Описание команды
    /// </summary>
    string Description { get; }
    
    /// <summary>
    ///     Выполнить команду
    /// </summary>
    Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args);
}