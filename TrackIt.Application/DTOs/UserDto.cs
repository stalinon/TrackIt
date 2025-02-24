namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO пользователя.
/// </summary>
public class UserDto
{
    /// <summary>
    ///     Уникальный идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = default!;
}