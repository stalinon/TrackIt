using System.Text.Json.Serialization;

namespace TrackIt.Application.DTOs;

/// <summary>
///     Модель пользователя
/// </summary>
public class UserDto
{
    /// <summary>
    ///     Электронная почта
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    ///     Дата создания
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Привязал Телеграм
    /// </summary>
    [JsonPropertyName("linked_telegram")]
    public bool LinkedTelegram { get; set; }
}