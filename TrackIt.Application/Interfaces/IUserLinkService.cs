namespace TrackIt.Application.Interfaces;

/// <summary>
///     Сервис линкования аккаунта к телеграмму
/// </summary>
public interface IUserLinkService
{
    /// <summary>
    ///     Генерация одноразового кода для привязки
    /// </summary>
    string GenerateLinkCode(long telegramId);

    /// <summary>
    ///     Проверка кода привязки
    /// </summary>
    Task<bool> ConfirmLinkAsync(string code, string email);
}