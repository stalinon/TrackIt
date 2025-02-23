using Microsoft.OpenApi.Models;

namespace TrackIt.API.Extensions;

/// <summary>
///     Расширения настройки Swagger
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    ///     Настроить Swagger
    /// </summary>
    public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackIt API", Version = "v1" });

            // Включаем XML-документацию для эндпоинтов
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TrackIt.API.xml"));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TrackIt.Application.xml"));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TrackIt.Domain.xml"));

            // Описание безопасности и других аспектов
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Authorization header using the Bearer scheme."
            });
        });

        return serviceCollection;
    }

    /// <summary>
    ///     Настроить Swagger
    /// </summary>
    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}