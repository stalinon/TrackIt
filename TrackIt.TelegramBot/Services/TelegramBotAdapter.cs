using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TrackIt.Application.Interfaces;
using TrackIt.TelegramBot.Handlers.Errors;
using TrackIt.TelegramBot.Handlers.Updates;

namespace TrackIt.TelegramBot.Services;

/// <summary>
///     Адаптер для работы с Telegram.Bot
/// </summary>
internal sealed class TelegramBotAdapter : ITelegramBotAdapter
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TelegramBotAdapter> _logger;

    /// <summary>
    ///     Обработчик входящих сообщений
    /// </summary>
    private Func<ITelegramBotClient, Update, CancellationToken, Task> UpdateHandler { get; set; }

    /// <summary>
    ///     Обработчик ошибок
    /// </summary>
    private Func<ITelegramBotClient, Exception, CancellationToken, Task> ErrorHandler { get; set; }

    /// <inheritdoc cref="TelegramBotAdapter" />
    public TelegramBotAdapter(string token, ILogger<TelegramBotAdapter> logger)
    {
        _botClient = new TelegramBotClient(token);
        _logger = logger;

        ErrorHandler = new CommonErrorHandler(logger);
        UpdateHandler = new CommonUpdateHandler(this);
    }

    /// <summary>
    ///     Запускает обработку сообщений от Telegram
    /// </summary>
    public void StartReceiving(CancellationToken cancellationToken)
    {
        try
        {
            _botClient.StartReceiving(
                UpdateHandler,
                ErrorHandler,
                new ReceiverOptions(),
                cancellationToken
            );

            _logger.LogInformation("Бот начал принимать сообщения...");
        }
        catch
        {
            _logger.LogCritical("Не удалось авторизовать бота");
        }
    }

    /// <summary>
    ///     Отправка сообщения пользователю
    /// </summary>
    public async Task SendMessageAsync(long chatId, string message)
    {
        try
        {
            await _botClient.SendMessage(chatId, message);
            _logger.LogInformation($"Сообщение отправлено пользователю {chatId}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при отправке сообщения пользователю {chatId}: {ex.Message}");
        }
    }
}