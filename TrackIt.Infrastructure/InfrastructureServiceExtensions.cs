using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Interfaces;
using TrackIt.Domain.Interfaces.Repositories;
using TrackIt.Infrastructure.Configurations;
using TrackIt.Infrastructure.Persistence;
using TrackIt.Infrastructure.Repositories;
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    /// <summary>
    ///     Запустить заполнение БД
    /// </summary>
    public static void ExecuteDatabaseSeed(this IServiceProvider serviceProvider)
    {
        serviceProvider.ApplyMigrations<ApplicationDbContext>();
        
        // Запуск DatabaseSeeder при старте приложения
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        seeder.Seed(context);
    }
    
    private static void ApplyMigrations<T>(this IServiceProvider serviceProvider) where T : DbContext
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<T>();

            db.Database.SetCommandTimeout(TimeSpan.FromDays(2));
            db.Database.Migrate();
            db.Database.SetCommandTimeout(null);
        }
        catch (Exception ex)
        {
            throw new Exception("Error applying database migrations", ex);
        }
    }
}