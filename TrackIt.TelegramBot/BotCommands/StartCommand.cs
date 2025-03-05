using Telegram.Bot;
using Telegram.Bot.Types;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Команда начала
/// </summary>
internal sealed class StartCommand : IBotCommand
{
    /// <inheritdoc />
    public string Command => "/start";
    
    /// <inheritdoc />
    public string Description => "Приветственное сообщение и информация о боте.";

    /// <inheritdoc />
    public async Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args)
    {
        await botClient.SendMessage(message.Chat.Id, 
            "👋 Привет! Я бот для управления бюджетом.\n" +
            "Введите /help для списка команд.");
    }
}