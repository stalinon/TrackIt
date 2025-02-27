using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrackIt.Application.Interfaces;
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
        services.AddSingleton<TelegramBotAdapter>(o
            => new TelegramBotAdapter(botToken!, o.GetRequiredService<ILogger<TelegramBotAdapter>>()));
        services.AddSingleton<ITelegramBotAdapter>(o => o.GetRequiredService<TelegramBotAdapter>());
        services.AddScoped<ITelegramNotificationService, TelegramNotificationService>();

        return services;
    }
    
    /// <summary>
    ///     Конфигурировать сервисы бота
    /// </summary>
    public static WebApplication UseTelegramBotServices(this WebApplication app)
    {
        var adapter = app.Services.GetRequiredService<TelegramBotAdapter>();
        adapter.StartReceiving(CancellationToken.None);
        return app;
    }
    
}