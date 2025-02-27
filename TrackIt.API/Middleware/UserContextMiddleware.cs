using Microsoft.AspNetCore.Authorization;
using TrackIt.Application.Interfaces;

namespace TrackIt.API.Middleware;

/// <summary>
///     Контекстная мидлварь
/// </summary>
public class UserContextMiddleware
{
    private readonly RequestDelegate _next;

    /// <inheritdoc cref="UserContextMiddleware" />
    public UserContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <inheritdoc cref="UserContextMiddleware" />
    public async Task Invoke(HttpContext context, IUserContext userContext)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() == null)
        {
            await _next(context);
            return;
        }
        
        try
        {
            // Загружаем пользователя перед выполнением запроса
            await userContext.GetOrCreateCurrentUserAsync();
        }
        catch (UnauthorizedAccessException)
        {
            // Если токен некорректен — прерываем запрос
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        // Передаем управление следующему middleware
        await _next(context);
    }
}