namespace TrackIt.Application.DTOs;

/// <summary>
///     Модель пользователя
/// </summary>
public class UserDto
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    ///     Электронная почта
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
}