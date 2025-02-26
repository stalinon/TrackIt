using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackIt.Application.DTOs;

namespace TrackIt.Application;

/// <summary>
///     Расширения <see cref="IServiceCollection" />
/// </summary>
public static class ApplicationServiceExtensions
{
    /// <summary>
    ///     Добавить сервисы TrackIt.Application в DI
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UserDto>());
        services.AddValidatorsFromAssemblyContaining<UserDto>();

        return services;
    }
}