using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using TrackIt.Application.Interfaces;
using TrackIt.TelegramBot.BotCommands;
using TrackIt.TelegramBot.Services;

namespace TrackIt.TelegramBot;

/// <summary>
///     Расширения <see cref="IServiceCollection"/>
/// </summary> 
public static class TelegramBotServiceExtensions
{
    /// <summary>
    ///     Добавить сервисы бота
    /// </summary>
    public static IServiceCollection AddTelegramBotServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var botToken = configuration["TelegramBot:Token"];
        services.AddSingleton<ITelegramBotClient, TelegramBotAdapter>(o
            => new TelegramBotAdapter(botToken!, o.GetRequiredService<ILogger<TelegramBotAdapter>>()));
        services.AddScoped<ITelegramNotificationService, TelegramNotificationService>();

        services.AddScoped<IBotCommand, StartCommand>();
        services.AddScoped<IBotCommand, AddIncomeCommand>();
        services.AddScoped<IBotCommand, AddExpenseCommand>();
        services.AddScoped<IBotCommand, HelpCommand>();
        services.AddScoped<CommandHandler>();

        return services;
    }
    
    /// <summary>
    ///     Конфигурировать сервисы бота
    /// </summary>
    public static WebApplication UseTelegramBotServices(this WebApplication app, IConfiguration configuration)
    {
        using var scope = app.Services.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        
        var domain = configuration["HostingDomain"]!;
        botClient.SetWebhook(domain).GetAwaiter().GetResult();

        Console.WriteLine("Webhook для бота установлен");
        return app;
    }
}