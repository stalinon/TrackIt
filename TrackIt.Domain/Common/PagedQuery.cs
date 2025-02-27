using Microsoft.AspNetCore.Mvc;

namespace TrackIt.Domain.Common;

/// <summary>
///     Базовый пагинированный запрос
/// </summary>
public abstract class PagedQuery
{
    /// <summary>
    ///     Номер страницы
    /// </summary>
    [FromQuery(Name = "page")]
    public int PageIndex { get; set; } = 0;

    /// <summary>
    ///     Предел
    /// </summary>
    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = 25;
}