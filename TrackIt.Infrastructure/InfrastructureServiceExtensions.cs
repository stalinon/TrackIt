using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackIt.Application.Interfaces;
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
        // Получаем строку подключения из конфигурации
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Добавляем DbContext в контейнер зависимостей
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserLinkService, UserLinkService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPlannedPaymentService, PlannedPaymentService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<IFinanceAnalyticsService, FinanceAnalyticsService>();
        
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        
        services.AddHangfire(config
            => config.UsePostgreSqlStorage(o
                => o.UseNpgsqlConnection(connectionString)));
        services.AddHangfireServer();

        return services;
    }

    /// <summary>
    ///     Конфигурировать сервисы инфраструктуры
    /// </summary>
    public static WebApplication UseInfrastructureServices(this WebApplication app)
    {
        app.UseHangfireDashboard();
        
        app.Services.ApplyMigrations<ApplicationDbContext>();
        return app;
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