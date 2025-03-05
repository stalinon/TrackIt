using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TrackIt.Application.DTOs;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IUserContext" />
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    /// <inheritdoc cref="UserContext" />
    public UserContext(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public Guid UserId =>
        _unitOfWork.Users.GetQuery().Where(u => u.Email == Email).Select(u => u.Id).First();

    /// <inheritdoc />
    public string? Email => _httpContextAccessor.HttpContext?.User.Claims
                                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                            ?? _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

    /// <inheritdoc />
    public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User.Claims
                                            .Where(c => c.Type == ClaimTypes.Role)
                                            .Select(c => c.Value)
                                        ?? new List<string>();

    /// <inheritdoc />
    public async Task<UserDto> GetOrCreateCurrentUserAsync()
    {
        if (string.IsNullOrEmpty(Email))
        {
            throw new UnauthorizedAccessException("Email not found in token");
        }

        // Проверяем еще раз внутри транзакции
        var user = await _unitOfWork.Users.GetByEmailAsync(Email);
        if (user != null)
        {
            return MapToUserDto(user);
        }

        // Блокируем создание нового пользователя внутри транзакции
        await using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                // Дважды проверяем, что пользователь не создался параллельно
                user = await _unitOfWork.Users.GetByEmailAsync(Email);
                if (user == null) // Если все еще нет, создаем
                {
                    user = new UserEntity
                    {
                        Id = Guid.NewGuid(),
                        Email = Email!,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _unitOfWork.Users.AddAsync(user);

                    // Фиксируем транзакцию
                    await _unitOfWork.CommitTransactionAsync();
                }
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        return MapToUserDto(user);
    }

    /// <inheritdoc />
    public async Task AuthorizeTelegramUserAsync(long telegramId)
    {
        var telegramUser = await _unitOfWork.TelegramUsers.GetQuery()
            .FirstOrDefaultAsync(t => t.TelegramId == telegramId);
        if (telegramUser?.User == null)
        {
            throw new UnauthorizedAccessException("Пользователь с таким Telegram ID не найден");
        }

        _httpContextAccessor.HttpContext!.User = new ClaimsPrincipal(new ClaimsIdentity([
            new Claim(ClaimTypes.Email, telegramUser.User.Email)
        ]));
    }


    private static UserDto MapToUserDto(UserEntity user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}