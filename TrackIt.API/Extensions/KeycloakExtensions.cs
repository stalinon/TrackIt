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
        var requireHttpsMetadata = bool.Parse(keycloakSettings["RequireHttpsMetadata"] ?? "false");
        Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.RequireHttpsMetadata = requireHttpsMetadata;
                options.MetadataAddress = $"{authority}/.well-known/openid-configuration";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authority,
                    ValidateAudience = false,
                    ValidateLifetime = true
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