using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TrackIt.Application;

namespace TrackIt.API.Extensions;

/// <summary>
///     Настройки Keycloak
/// </summary>
public static class KeycloakExtensions
{
    /// <summary>
    ///     Добавляет поддержку аутентификации через Keycloak
    /// </summary>
    public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var keycloakSettings = configuration.GetSection("Keycloak");

        var authority = keycloakSettings["Authority"];
        var audience = keycloakSettings["Audience"];
        var requireHttpsMetadata = bool.Parse(keycloakSettings["RequireHttpsMetadata"] ?? "false");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
                options.RequireHttpsMetadata = requireHttpsMetadata;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuer = true,
                    ValidIssuer = authority,
                    ValidateLifetime = true,
                    RoleClaimType = "roles"
                };
            });

        return services;
    }

    /// <summary>
    ///     Добавляет поддержку авторизации в приложение
    /// </summary>
    public static IServiceCollection AddKeycloakAuthorization(this IServiceCollection services)
    {
        AuthPolicies.AddAuthorization(services);

        return services;
    }
}