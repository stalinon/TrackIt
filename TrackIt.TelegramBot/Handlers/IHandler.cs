namespace TrackIt.TelegramBot.Handlers;

/// <summary>
///     Обработчик сообщений
/// </summary>
public abstract class Handler
{
    protected Handler? Next;
    
    /// <summary>
    ///     Установить следующий обработчик
    /// </summary>
    /// <returns>
    ///     Возвращает этот же для построения цепочки
    /// </returns>
    public Handler? SetNext(Handler? next)
    {
        Next = next;
        return next;
    }
}