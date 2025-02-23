using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackIt.Infrastructure.Configurations;
using TrackIt.Infrastructure.Persistence;
using TrackIt.Infrastructure.Services;

namespace TrackIt.Infrastructure;

/// <summary>
///     Расширения <see cref="IServiceCollection"/>
/// </summary> 
public static class InfrastructureServiceExtensions
{
    /// <summary>
    ///     Добавить сервисы инфраструктуры
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        
        // Получаем строку подключения из конфигурации
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Добавляем DbContext в контейнер зависимостей
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddTransient<DatabaseSeeder>();

        return services;
    }

    /// <summary>
    ///     Запустить заполнение БД
    /// </summary>
    public static void ExecuteDatabaseSeed(this IServiceProvider serviceProvider)
    {
        // Запуск DatabaseSeeder при старте приложения
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        seeder.Seed(context);
    }
}