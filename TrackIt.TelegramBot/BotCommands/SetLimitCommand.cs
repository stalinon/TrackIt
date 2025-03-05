using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using TrackIt.Application.Features.Budgets.Commands;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Enums;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Установка лимита на категорию
/// </summary>
internal sealed class SetLimitCommand(IUnitOfWork unitOfWork, IBudgetService budgetService) : IBotCommand
{
    /// <inheritdoc />
    public string Command => "/set_limit";
    
    /// <inheritdoc />
    public string Description => "Установить лимит. Использование: /set_limit <категория> <сумма>";

    /// <inheritdoc />
    public async Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args)
    {
        if (args.Length < 2)
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Введите команду в формате: /set_limit <категория> <сумма>");
            return;
        }

        var category = args[0];
        if (!decimal.TryParse(args[1], out var amount))
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Сумма должна быть числом.");
            return;
        }
        
        var categoryEntity = await unitOfWork.Categories.GetQuery()
            .FirstOrDefaultAsync(c => c.Name == category && c.Type == CategoryType.EXPENSE);
        if (categoryEntity == null)
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Категория не найдена.");
            return;
        }

        await budgetService.CreateAsync(new CreateBudgetCommand
        {
            CategoryId = categoryEntity.Id,
            LimitAmount = amount
        }, CancellationToken.None);

        await botClient.SendMessage(message.Chat.Id, $"✅ Лимит на {category} установлен: {amount} руб.");
    }
}