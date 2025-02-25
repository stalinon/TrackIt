using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace TrackIt.Infrastructure.Services;

/// <summary>
///     Сервис аутентификации Keycloak
/// </summary>
public class KeycloakAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    /// <inheritdoc cref="KeycloakAuthService" />
    public KeycloakAuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    ///     Получение admin access token для управления пользователями
    /// </summary>
    private async Task<string> GetAdminAccessTokenAsync()
    {
        var keycloakSettings = _configuration.GetSection("Keycloak");
        var clientId = keycloakSettings["ClientId"];
        var clientSecret = keycloakSettings["ClientSecret"];

        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", clientId! },
            { "client_secret", clientSecret! }
        });

        var response = await _httpClient.PostAsync($"{keycloakSettings["Authority"]}/protocol/openid-connect/token", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);

        return json!["access_token"];
    }

    /// <summary>
    ///     Регистрация нового пользователя
    /// </summary>
    public async Task<string?> RegisterUserAsync(string username, string email, string password)
    {
        var accessToken = await GetAdminAccessTokenAsync();
        var keycloakSettings = _configuration.GetSection("Keycloak");
        var realm = keycloakSettings["Authority"]!.Split('/').Last();

        var user = new
        {
            username,
            email,
            enabled = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = password,
                    temporary = false
                }
            }
        };

        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.PostAsync($"{keycloakSettings["Authority"]}/admin/realms/{realm}/users", content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        if (response.Headers.Location == null)
        {
            return null;
        }

        // Извлекаем ID пользователя из заголовка Location
        var locationUrl = response.Headers.Location.ToString();
        var userId = locationUrl.Split('/').Last();
        return userId;

    }

    /// <summary>
    ///     Выполнение входа (логина) пользователя
    /// </summary>
    public async Task<string?> LoginAsync(string username, string password)
    {
        var keycloakSettings = _configuration.GetSection("Keycloak");
        var clientId = keycloakSettings["Audience"];

        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", clientId! },
            { "username", username },
            { "password", password }
        });

        var response = await _httpClient.PostAsync($"{keycloakSettings["Authority"]}/protocol/openid-connect/token", content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        Dictionary<string, string?>? json = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString)!;
        return json!["access_token"];
    }
}
