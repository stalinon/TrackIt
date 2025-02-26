namespace TrackIt.Domain.Common;

/// <summary>
///     Базовый пагинированный запрос
/// </summary>
public abstract class PagedQuery
{
    /// <summary>
    ///     Номер страницы
    /// </summary>
    public int PageIndex { get; set; } = 0;

    /// <summary>
    ///     Предел
    /// </summary>
    public int Limit { get; set; } = 25;
}