using TrackIt.Application.Interfaces;

namespace TrackIt.TelegramBot.Services;

/// <inheritdoc cref="ITelegramNotificationService"/>
internal sealed class TelegramNotificationService(ITelegramBotAdapter adapter, IUnitOfWork unitOfWork) : ITelegramNotificationService
{
    /// <inheritdoc />
    public async Task SendNotificationAsync(Guid userId, string message, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(userId);
        var telegramChatId = user!.TelegramUser.TelegramId;
        await adapter.SendMessageAsync(telegramChatId, message);
    }
}