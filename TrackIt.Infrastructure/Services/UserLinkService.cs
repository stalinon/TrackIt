using System.Collections.Concurrent;
using System.Security.Cryptography;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IUserLinkService"/>
internal sealed class UserLinkService(IUnitOfWork unitOfWork) : IUserLinkService
{
    private static readonly ConcurrentDictionary<string, (long TelegramId, DateTime Expiry)> LinkCodes = new();

    /// <summary>
    ///     Генерация одноразового кода для привязки
    /// </summary>
    public string GenerateLinkCode(long telegramId)
    {
        var code = GenerateRandomCode();
        LinkCodes[code] = (telegramId, DateTime.UtcNow.AddMinutes(10)); // Код действителен 10 минут
        return code;
    }

    /// <summary>
    ///     Проверка кода привязки
    /// </summary>
    public async Task<bool> ConfirmLinkAsync(string code, string email)
    {
        if (!LinkCodes.TryRemove(code, out var linkData))
        {
            return false; // Код недействителен
        }

        var user = await unitOfWork.Users.GetByEmailAsync(email);
        if (user == null)
        {
            return false;
        }

        var telegramUser = new TelegramUserEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TelegramId = linkData.TelegramId
        };

        await unitOfWork.TelegramUsers.AddAsync(telegramUser);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static string GenerateRandomCode()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(3);
        return BitConverter.ToUInt32(randomBytes, 0).ToString()[..6];
    }
}