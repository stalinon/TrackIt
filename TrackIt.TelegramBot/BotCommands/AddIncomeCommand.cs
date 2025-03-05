using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using TrackIt.Application.Features.Transactions.Commands;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Enums;

namespace TrackIt.TelegramBot.BotCommands;

/// <summary>
///     Добавление дохода
/// </summary>
internal sealed class AddIncomeCommand(IUnitOfWork unitOfWork, ITransactionService transactionService) : IBotCommand
{
    /// <inheritdoc />
    public string Command => "/add_income";
    
    /// <inheritdoc />
    public string Description => "Добавить доход. Использование: /add_income <категория> <сумма> <описание>";

    /// <inheritdoc />
    public async Task ExecuteAsync(ITelegramBotClient botClient, Message message, string[] args)
    {
        if (args.Length < 3)
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Используйте /add_income <категория> <сумма> <описание>");
            return;
        }
        
        var categoryEntity = await unitOfWork.Categories.GetQuery()
            .FirstOrDefaultAsync(c => c.Name == args[0] && c.Type == CategoryType.INCOME);
        if (categoryEntity == null)
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Категория не найдена.");
            return;
        }

        if (!decimal.TryParse(args[1], out var amount))
        {
            await botClient.SendMessage(message.Chat.Id, "⚠ Ошибка: Введите корректную сумму.");
            return;
        }
        
        var description = string.Join(" ", args.Skip(1));
        await transactionService.CreateAsync(new CreateTransactionCommand
        {
            CategoryId = categoryEntity.Id,
            Description = description,
            Amount = amount,
            Date = DateTime.UtcNow
        }, CancellationToken.None);

        await botClient.SendMessage(message.Chat.Id, $"✅ Доход добавлен в категорию {categoryEntity.Name}: {amount} у.е. ({description})");
    }
}