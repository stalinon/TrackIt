using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Обработчик команд
/// </summary>
public partial class CommandHandler
{
    [GeneratedRegex(@"[\""].+?[\""]|\S+")]
    private static partial Regex HandleCommand();
    
    private readonly ITelegramBotClient _botClient;
    private readonly Dictionary<string, IBotCommand> _commands;

    /// <inheritdoc cref="CommandHandler" />
    public CommandHandler(IEnumerable<IBotCommand> commands, ITelegramBotClient botClient)
    {
        _botClient = botClient;

        var commandList = commands.ToList();
        var helpCommand = new HelpCommand(commandList);
        commandList.Add(helpCommand);
        _commands = commandList.ToDictionary(
            cmd => cmd.Command,
            cmd => cmd);
    }

    /// <summary>
    ///     Обработать сообщение
    /// </summary>
    public async Task HandleAsync(Message message)
    {
        var parts = ParseArguments(message.Text!);
        if (parts.Length == 0)
        {
            return;
        }

        var command = parts[0].ToLower();
        var args = parts.Skip(1).ToArray();

        if (_commands.TryGetValue(command, out var cmd))
        {
            await cmd.ExecuteAsync(_botClient, message, args);
        }
        else
        {
            await _botClient.SendMessage(message.Chat.Id, "❌ Неизвестная команда. Введите /help.");
        }
    }

    private static string[] ParseArguments(string input)
    {
        var matches = HandleCommand().Matches(input);

        return matches.Select(m => m.Value.Trim('"')).ToArray();
    }
}