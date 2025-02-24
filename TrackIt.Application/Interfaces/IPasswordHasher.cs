namespace TrackIt.Application.Interfaces;

/// <summary>
///     Интерфейс для работы с паролями.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    ///     Хэшировать пароль
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <returns>Хэш пароля</returns>
    string HashPassword(string password);
    
    /// <summary>
    ///     Верифицировать пароль
    /// </summary>
    /// <param name="hashedPassword">Хэшированный пароль</param>
    /// <param name="providedPassword">Данный пароль</param>
    /// <returns>Статус верификации</returns>
    bool VerifyPassword(string hashedPassword, string providedPassword);
}