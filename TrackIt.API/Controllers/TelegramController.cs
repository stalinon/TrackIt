using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TrackIt.API.Attributes;
using TrackIt.TelegramBot.BotCommands;

namespace TrackIt.API.Controllers;

/// <summary>
///     Контроллер Телеграма
/// </summary>
[ApiController]
[TelegramWebhook]
[Route("api/bot")]
public class TelegramController(CommandHandler commandHandler) : ControllerBase
{
    /// <summary>
    ///     Обработчик вебхука
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        if (update is { Type: UpdateType.Message, Message.Text: not null })
        {
            await commandHandler.HandleAsync(update.Message);
        }

        return Ok();
    }
}