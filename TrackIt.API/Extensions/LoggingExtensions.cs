using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace TrackIt.API.Extensions;

/// <summary>
///     Настройки логирования
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    ///     Добавить логирование
    /// </summary>
    public static void AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        // Конфигурация Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() // Минимальный уровень логирования
            .WriteTo.Console(theme: AnsiConsoleTheme.Code) // Вывод логов в консоль
            .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Day) // Логи в файл с ежедневной ротацией
            .Enrich.FromLogContext() // Добавляем контекстные данные, такие как пользовательские данные
            .CreateLogger();

        services.AddLogging(builder => builder.AddSerilog());
    }
}