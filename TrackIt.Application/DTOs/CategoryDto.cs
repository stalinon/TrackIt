namespace TrackIt.Application.DTOs;

/// <summary>
///     DTO для категории.
/// </summary>
public class CategoryDto
{
    /// <summary>
    ///     Уникальный идентификатор категории.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Уникальный идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Название категории.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    ///     Тип категории (income/expense).
    /// </summary>
    public string Type { get; set; } = default!;
}