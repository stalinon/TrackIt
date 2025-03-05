using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Requests.Abstractions;
using TrackIt.Application.Interfaces;

namespace TrackIt.TelegramBot.Services;

/// <summary>
///     Адаптер для работы с Telegram.Bot
/// </summary>
internal sealed class TelegramBotAdapter : TelegramBotClient
{
    private readonly ILogger<TelegramBotAdapter> _logger;

    /// <inheritdoc cref="TelegramBotAdapter" />
    public TelegramBotAdapter(string token, ILogger<TelegramBotAdapter> logger) : base(token)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SendRequest(request, cancellationToken);
            _logger.LogInformation("Запрос в Telegram успешно отправлен");
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при отправке запроса в Telegram: {ex.Message}");
            throw;
        }
    }
}