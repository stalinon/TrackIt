using Microsoft.Extensions.DependencyInjection;

namespace TrackIt.Application;

/// <summary>
///     Политики авторизации
/// </summary>
public static class AuthPolicies
{
    /// <summary>
    ///     Политика администратора
    /// </summary>
    public const string AdminPolicy = nameof(AdminPolicy);

    /// <summary>
    ///     Политика пользователя
    /// </summary>
    public const string UserPolicy = nameof(UserPolicy);

    /// <summary>
    ///     Добавляет поддержку авторизации в приложение
    /// </summary>
    public static void AddAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AdminPolicy, policy =>
                policy.RequireClaim("roles", "admin"));

            options.AddPolicy(UserPolicy, policy =>
                policy.RequireClaim("roles", "user"));
        });
    }
}