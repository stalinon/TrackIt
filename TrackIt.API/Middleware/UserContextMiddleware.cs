using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Telegram.Bot.Types;
using TrackIt.API.Attributes;
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
            if (endpoint?.Metadata.GetMetadata<TelegramWebhookAttribute>() != null)
            {
                var authorized = await HandleTelegramAuthorization(context, userContext);
            }
            
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

    private async Task<bool> HandleTelegramAuthorization(HttpContext context, IUserContext userContext)
    {
        try
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            var update = JsonSerializer.Deserialize<Update>(body);
            if (update?.Message?.From?.Id is { } userId)
            {
                await userContext.AuthorizeTelegramUserAsync(userId);
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка Middleware: {ex.Message}");
        }
        
        return false;
    }
}